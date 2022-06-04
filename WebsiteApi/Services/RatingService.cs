using Microsoft.ML;
using Microsoft.ML.Trainers;
using System;
using System.Collections.Generic;
using System.IO;
using WebsiteApi.Model.Dtos;
using WebsiteApi.Services.IServices;

namespace WebsiteApi.Services
{
    public class RatingService: IRatingService
    {
        
        public void Recommend(List<ProductRating> value)
        {
            // Create MLContext to be shared across the model creation workflow objects
            // <SnippetMLContext>
            MLContext mlContext = new MLContext();
            // </SnippetMLContext>

            // Load data
            // <SnippetLoadDataMain>
            (IDataView trainingDataView, IDataView testDataView) = LoadData(mlContext);
            // </SnippetLoadDataMain>

            // Build & train model
            // <SnippetBuildTrainModelMain>
            ITransformer model = BuildAndTrainModel(mlContext, trainingDataView);
            // </SnippetBuildTrainModelMain>

            // Evaluate quality of model
            // <SnippetEvaluateModelMain>
            EvaluateModel(mlContext, testDataView, model);
            // </SnippetEvaluateModelMain>

            // Use model to try a single prediction (one row of data)
            // <SnippetUseModelMain>
            var id = UseModelForSinglePrediction(mlContext, model, value);
            // </SnippetUseModelMain>

            // Save model
            // <SnippetSaveModelMain>
            SaveModel(mlContext, trainingDataView.Schema, model);
            // </SnippetSaveModelMain>
        }
        public  (IDataView training, IDataView test) LoadData(MLContext mlContext)
        { 
            // Load training & test datasets using datapaths
            // <SnippetLoadData>
            var trainingDataPath = Path.Combine(Environment.CurrentDirectory, "Data", "recommendation-ratings-train.csv");
            var testDataPath = Path.Combine(Environment.CurrentDirectory, "Data", "recommendation-ratings-test.csv");

            IDataView trainingDataView = mlContext.Data.LoadFromTextFile<ProductRating>(trainingDataPath, hasHeader: true, separatorChar: ',');
            IDataView testDataView = mlContext.Data.LoadFromTextFile<ProductRating>(testDataPath, hasHeader: true, separatorChar: ',');

            return (trainingDataView, testDataView);
            // </SnippetLoadData>
        }

        // Build and train model
        public  ITransformer BuildAndTrainModel(MLContext mlContext, IDataView trainingDataView)
        {
            // Add data transformations
            // <SnippetDataTransformations>
            IEstimator<ITransformer> estimator = mlContext.Transforms.Conversion.MapValueToKey(outputColumnName: "userIdEncoded", inputColumnName: "userId")
                .Append(mlContext.Transforms.Conversion.MapValueToKey(outputColumnName: "productIdEncoded", inputColumnName: "productId"));
            // </SnippetDataTransformations>

            // Set algorithm options and append algorithm
            // <SnippetAddAlgorithm>
            var options = new MatrixFactorizationTrainer.Options
            {
                MatrixColumnIndexColumnName = "userIdEncoded",
                MatrixRowIndexColumnName = "productIdEncoded",
                LabelColumnName = "Label",
                NumberOfIterations = 20,
                ApproximationRank = 100
            };

            var trainerEstimator = estimator.Append(mlContext.Recommendation().Trainers.MatrixFactorization(options));
            // </SnippetAddAlgorithm>

            // <SnippetFitModel>
            Console.WriteLine("=============== Training the model ===============");
            ITransformer model = trainerEstimator.Fit(trainingDataView);

            return model;
            // </SnippetFitModel>
        }

        // Evaluate model
        public  void EvaluateModel(MLContext mlContext, IDataView testDataView, ITransformer model)
        {
            // Evaluate model on test data & print evaluation metrics
            // <SnippetTransform>
            Console.WriteLine("=============== Evaluating the model ===============");
            var prediction = model.Transform(testDataView);
            // </SnippetTransform>

            // <SnippetEvaluate>
            var metrics = mlContext.Regression.Evaluate(prediction, labelColumnName: "Label", scoreColumnName: "Score");
            // </SnippetEvaluate>

            // <SnippetPrintMetrics>
            Console.WriteLine("Root Mean Squared Error : " + metrics.RootMeanSquaredError.ToString());
            Console.WriteLine("RSquared: " + metrics.RSquared.ToString());
            // </SnippetPrintMetrics>
        }

        // Use model for single prediction
        public  float  UseModelForSinglePrediction(MLContext mlContext, ITransformer model, List<ProductRating> value)
        {
            // <SnippetPredictionEngine>
            Console.WriteLine("=============== Making a prediction ===============");
            var predictionEngine = mlContext.Model.CreatePredictionEngine<ProductRating, ProductRatingPrediction>(model);
            // </SnippetPredictionEngine>

            // Create test input & make single prediction
            // <SnippetMakeSinglePrediction>
            //var testInput = new ProductRating { userId = 4, productId = 2424 };

            foreach (var item in value)
            {
                var productRatingPrediction = predictionEngine.Predict(item);
                // </SnippetMakeSinglePrediction>

                // <SnippetPrintResults>
                if (Math.Round(productRatingPrediction.Score, 1) > 3.5)
                {
                    Console.WriteLine("product " + item.productId + " is recommended for user " + item.userId);
                }
                else
                {
                    Console.WriteLine("product " + item.productId + " is not recommended for user " + item.userId);
                }
            }
            return value.Count;
            // </SnippetPrintResults>
        }

        //Save model
        public  void SaveModel(MLContext mlContext, DataViewSchema trainingDataViewSchema, ITransformer model)
        {
            // Save the trained model to .zip file
            // <SnippetSaveModel>
            var modelPath = Path.Combine(Environment.CurrentDirectory, "Data", "productRecommenderModel.zip");

            Console.WriteLine("=============== Saving the model to a file ===============");
            mlContext.Model.Save(model, trainingDataViewSchema, modelPath);
            // </SnippetSaveModel>
        }
    }
}
