using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
using System.IO;

namespace BasicSynth
{
    public partial class BasicSynth : Form
    {
        private const int SAMPLE_RATE = 44100;
        private const short BITS_PER_SAMPLE = 16;
        Random random = new Random();

        public BasicSynth()
        {
            InitializeComponent();
        }

        private void BasicSynthForm_KeyDown(object sender, KeyEventArgs e)
        {
            IEnumerable<Oscillator> oscillators = this.Controls.OfType<Oscillator>();
            float frequency = 440f;
            int oscillatorsCount = oscillators.Count();
            short[] wave = new short[SAMPLE_RATE];
            byte[] binaryWave = new byte[SAMPLE_RATE * sizeof(short)];
            
            // keys: z s x d c f v b h n j m ,
            switch (e.KeyCode)
            {
                case Keys.Z:
                    frequency = 130.8f;     // C3
                    break;
                case Keys.S:
                    frequency = 138.5f;     // C#3
                    break;
                case Keys.X:
                    frequency = 146.8f;     // D3
                    break;
                case Keys.D:
                    frequency = 155.5f;     // D#3
                    break;
                case Keys.C:
                    frequency = 164.8f;     // E3
                    break;
                case Keys.V:
                    frequency = 174.6f;     // F3
                    break;
                case Keys.G:
                    frequency = 184.9f;     // F#3
                    break;
                case Keys.B:
                    frequency = 195.9f;     // G3
                    break;
                case Keys.H:
                    frequency = 207.6f;     // G#3
                    break;
                case Keys.N:
                    frequency = 220.0f;     // A3
                    break;
                case Keys.J:
                    frequency = 233.0f;     // Bb3
                    break;
                case Keys.M:
                    frequency = 246.9f;     // B3
                    break;
                case Keys.Oemcomma:
                    frequency = 261.6f;     // C4
                    break;
            }

            foreach (Oscillator oscillator in oscillators)
            {
                int samplesPerWaveLength = (int)(SAMPLE_RATE / frequency);
                short ampStep = (short)((short.MaxValue * 2) / samplesPerWaveLength);
                short tempSample;

                // Waveform Algorithms
                switch (oscillator.WaveForm)
                {
                    case WaveForm.Sine:
                        for (int i = 0; i < SAMPLE_RATE; i++)
                        {
                            wave[i] += Convert.ToInt16((short.MaxValue * Math.Sin(((Math.PI * 2 * frequency) / SAMPLE_RATE) * i)) / oscillatorsCount);
                        }
                        break;

                    case WaveForm.Square:
                        for (int i = 0; i < SAMPLE_RATE; i++)
                        {
                            wave[i] += Convert.ToInt16((short.MaxValue * Math.Sign(Math.Sin((Math.PI * 2 * frequency) / SAMPLE_RATE * i))) / oscillatorsCount);
                }
                        break;

                    case WaveForm.Saw:
                        for (int i = 0; i < SAMPLE_RATE; i++)
                        {
                            tempSample = -short.MaxValue;
                            for (int j = 0; j < samplesPerWaveLength && i < SAMPLE_RATE; j++)
                            {
                                tempSample += ampStep;
                                wave[i++] += Convert.ToInt16(tempSample / oscillatorsCount);
                            }
                            i--;
                        }
                        break;

                    case WaveForm.Triangle:
                        tempSample = -short.MaxValue;
                        for (int i = 0; i < SAMPLE_RATE; i++)
                        {
                            if (Math.Abs(tempSample + ampStep) > short.MaxValue / oscillatorsCount)
                            {
                                ampStep = (short)-ampStep;
                            }
                            tempSample += ampStep;
                            wave[i] += Convert.ToInt16(tempSample);
                        }
                        break;

                    case WaveForm.Noise:
                        for (int i = 0; i < SAMPLE_RATE; i++)
                        {
                            wave[i] += Convert.ToInt16(random.Next(-short.MaxValue, short.MaxValue) / oscillatorsCount);
                }
                        break;
                }
            }
            Buffer.BlockCopy(wave, 0, binaryWave, 0, wave.Length * sizeof(short));
            using (MemoryStream memoryStream = new MemoryStream())
            using (BinaryWriter binaryWriter = new BinaryWriter(memoryStream))
            {
                short blockAlign = BITS_PER_SAMPLE / 8;
                int subChunkTwoSize = SAMPLE_RATE * blockAlign;
                binaryWriter.Write(new[] { 'R', 'I', 'F', 'F' });
                binaryWriter.Write(36 + subChunkTwoSize);
                binaryWriter.Write(new[] { 'W', 'A', 'V', 'E', 'f', 'm', 't', ' ' });
                binaryWriter.Write(16);
                binaryWriter.Write((short)1);
                binaryWriter.Write((short)1);
                binaryWriter.Write(SAMPLE_RATE);
                binaryWriter.Write(SAMPLE_RATE * blockAlign);
                binaryWriter.Write(blockAlign);
                binaryWriter.Write(BITS_PER_SAMPLE);
                binaryWriter.Write(new[] { 'd', 'a', 't', 'a' });
                binaryWriter.Write(subChunkTwoSize);
                binaryWriter.Write(binaryWave);
                memoryStream.Position = 0;
                new SoundPlayer(memoryStream).Play();
            }
        }
    }

    public enum WaveForm
    {
        Sine, Saw, Square, Triangle, Noise
    }
}
