using System;

public delegate void BuildStatusHandler(string message);

public class BuildStatus {
    public event BuildStatusHandler? BuildCompleted;
    public event BuildStatusHandler? BuildFailed;

    public void BuildSuccess(string message) {
        BuildCompleted?.Invoke(message);
    }

    public void BuildFailure(string message) {
        BuildFailed?.Invoke(message);
    }
}

class Logger {
    public void Log(string message) {
        Console.WriteLine("[LOG] :" + message);
    }
}

class Notifier {
    public void Notify(string message) {
        Console.WriteLine("[NOTIFICATION] :" + message);
    }
}

class RetryEngine {
    public void Retry(string message) {
        Console.WriteLine("[RETRY ENGINE] Retrying the build due to: " + message);
    }
}

class Program {

    public static void Main(string[] args) {
        Logger log = new Logger();
        Notifier notifier = new Notifier();
        RetryEngine retry = new RetryEngine();
        BuildStatus buildStatus = new BuildStatus();

        buildStatus.BuildCompleted += log.Log;
        buildStatus.BuildCompleted += notifier.Notify;

        buildStatus.BuildFailed += log.Log;
        buildStatus.BuildFailed += notifier.Notify;
        buildStatus.BuildFailed += retry.Retry;
        
        int counter = 0;
        int retryLimit = 5;

        while (counter < retryLimit) {
            Console.WriteLine($"\nAttempt #{counter + 1} at building...");
            if (counter < 2) {
                buildStatus.BuildFailure($"Build failed at attempt {counter + 1}. Issue still unresolved.");
                Console.WriteLine("Waiting for 2 seconds before retrying...");
                Thread.Sleep(2000);
            } else {
                buildStatus.BuildSuccess($"Build completed successfully at attempt {counter + 1}.");
                break;
            }
            counter++;
        }
    }
}
