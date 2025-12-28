using ClashOfClaps.Data.Options;
using Microsoft.Extensions.Options;
using PortAudioSharp;
using Stream = PortAudioSharp.Stream;

namespace ClashOfClaps.Data.DataProviders;

public sealed class AudioMeterDataProvider : IDisposable
{
    private readonly Stream _stream;
    private volatile float _peak;
    private volatile float _rms;

    public float PeakDbFs => ToDbFs(_peak);
    public float RmsDbFs => ToDbFs(_rms);

    public AudioMeterDataProvider(IOptions<ApplicationOptions> options)
    {
        PortAudio.Initialize();

        var inputParams = new StreamParameters
        {
            device = PortAudio.DefaultInputDevice,
            channelCount = options.Value.InputChannel,
            sampleFormat = SampleFormat.Float32,
            suggestedLatency = PortAudio.GetDeviceInfo(PortAudio.DefaultInputDevice).defaultLowInputLatency,
        };

        _stream = new Stream(
            inputParams,
            null,
            options.Value.SampleRate,
            1024,
            StreamFlags.ClipOff,
            AudioCallback,
            null
        );

        _stream.Start();
    }

    private static float ToDbFs(float value)
    {
        return value <= 0 ? -100f : 20f * (float)Math.Log10(value);
    }

    private StreamCallbackResult AudioCallback(
        IntPtr input,
        IntPtr output,
        uint frameCount,
        ref StreamCallbackTimeInfo timeInfo,
        StreamCallbackFlags statusFlags,
        IntPtr userData)
    {
        unsafe
        {
            var samples = (float*)input;

            var peak = 0f;
            double sumSquares = 0;

            for (int i = 0; i < frameCount; i++)
            {
                var s = Math.Abs(samples[i]);
                if (s > peak) peak = s;
                sumSquares += s * s;
            }

            _peak = peak;
            _rms = (float)Math.Sqrt(sumSquares / frameCount);
        }

        return StreamCallbackResult.Continue;
    }

    public void Dispose()
    {
        _stream?.Stop();
        _stream?.Dispose();
        PortAudio.Terminate();
    }
}
