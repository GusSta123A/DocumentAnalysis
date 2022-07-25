//set `<your-endpoint>` and `<your-key>` variables with the values from the Azure portal to create your `AzureKeyCredential` and `DocumentAnalysisClient` instance
using Azure;
using Azure.AI.FormRecognizer.DocumentAnalysis;

string endpoint = "https://fr-formrecognizertest.cognitiveservices.azure.com/";
string key = "3ea936ec896944019b2241e3b5d923ad";
AzureKeyCredential credential = new AzureKeyCredential(key);
DocumentAnalysisClient client = new DocumentAnalysisClient(new Uri(endpoint), credential);


//sample form document
Uri fileUri = new Uri("C:\\Users\\adminuser\\Desktop\\FORMULARIO - Permiso.pdf");

string filePath = "C:\\Users\\adminuser\\Desktop\\FORMULARIO - Permiso.pdf";

AnalyzeDocumentOperation operation = await client.StartAnalyzeDocumentFromUriAsync("prebuilt-document", fileUri);

await operation.WaitForCompletionAsync();

AnalyzeResult result = operation.Value;

Console.WriteLine("Detected key-value pairs:");

foreach (DocumentKeyValuePair kvp in result.KeyValuePairs)
{
    if (kvp.Value == null)
    {
        Console.WriteLine($"  Found key with no value: '{kvp.Key.Content}'");
    }
    else
    {
        Console.WriteLine($"  Found key-value pair: '{kvp.Key.Content}' and '{kvp.Value.Content}'");
    }
}
