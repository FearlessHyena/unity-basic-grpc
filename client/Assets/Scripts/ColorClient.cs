using Grpc.Core;
using Protocolor;
using UnityEngine;

public class ColorClient {
    
    private readonly ColorGenerator.ColorGeneratorClient _client;
    private readonly Channel _channel;
    private readonly string _server = "127.0.0.1:50051";

    internal ColorClient() {
        _channel = new Channel(_server, ChannelCredentials.Insecure);
        _client = new ColorGenerator.ColorGeneratorClient(_channel);
    }

    internal string GetRandomColor(string currentColor) {
        var randomColor = _client.GetRandomColor(new CurrentColor {Color = currentColor});
        Debug.Log("Client is currently using color: " + currentColor +
                  " switching to: " + randomColor.Color);

        return randomColor.Color;
    }

    private void OnDisable() {
        _channel.ShutdownAsync().Wait();
    }
}