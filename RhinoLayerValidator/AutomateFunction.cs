using Objects;
using Speckle.Automate.Sdk;
using Speckle.Core.Models.Extensions;

public static class AutomateFunction
{
    public static async Task Run(AutomationContext automationContext, FunctionInputs functionInputs)
    {
        Console.WriteLine("Starting execution");
        _ = typeof(ObjectsKit).Assembly; // INFO: Force objects kit to initialize

        Console.WriteLine("Receiving version");
        var commitObject = await automationContext.ReceiveVersion();

        Console.WriteLine("Received version: " + commitObject);

        var objects = commitObject.Flatten().Where(x => x["collectionType"] != null);

        var invalidLayers = commitObject
            .Flatten()
            .Select(c => c.TryGetName())
            .Where(functionInputs.ValidLayers.Contains)
            .ToList();


        if (invalidLayers.Count() > 0)
        {
            Console.WriteLine("Found these yucky layers: ");
            foreach (var item in invalidLayers)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine($"In total there were {0} yucky layers in the model.", invalidLayers.Count());
            automationContext.MarkRunFailed($"Failed because there were yucky layers, be better");
            return;
        }

        automationContext.MarkRunSuccess($"Models all good...");
    }
}
