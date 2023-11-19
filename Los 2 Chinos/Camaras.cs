using System;
using System.Drawing;
using System.Windows.Forms;
using AForge.Video;
using AForge.Video.DirectShow;

namespace Los_2_Chinos
{
    public partial class Camaras : Form
    {
        private FilterInfoCollection videoDevices;
        private VideoCaptureDevice[] videoSource;

        public Camaras()
        {
            InitializeComponent();
            InitializeCamera();
        }

        private void InitializeCamera()
        {
            videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            videoSource = new VideoCaptureDevice[4];

            for (int i = 0; i < Math.Min(4, videoDevices.Count); i++)
            {
                videoSource[i] = new VideoCaptureDevice(videoDevices[i].MonikerString);
                videoSource[i].NewFrame += new NewFrameEventHandler(video_NewFrame);
            }

            // Llena el ComboBox con las cámaras encontradas
            for (int i = 0; i < Math.Min(4, videoDevices.Count); i++)
            {
                comboBox5.Items.Add(videoDevices[i].Name);
            }

            if (comboBox5.Items.Count > 0)
            {
                comboBox5.SelectedIndex = 0;
            }
        }

        private void comboBoxCamaras_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Detén todas las cámaras
            foreach (var source in videoSource)
            {
                if (source != null && source.IsRunning)
                {
                    source.SignalToStop();
                    source.WaitForStop();
                }
            }

            // Inicia la cámara seleccionada en el ComboBox
            int selectedCameraIndex = comboBox5.SelectedIndex;
            if (selectedCameraIndex >= 0 && selectedCameraIndex < videoSource.Length)
            {
                videoSource[selectedCameraIndex].Start();
            }
        }

        private void video_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            // Actualiza el PictureBox correspondiente con el fotograma actual
            if (pictureBox1.Image != null)
            {
                pictureBox1.Image.Dispose(); // Libera la imagen anterior
            }

            if (pictureBox2.Image != null)
            {
                pictureBox2.Image.Dispose(); // Libera la imagen anterior
            }

            if (pictureBox3.Image != null)
            {
                pictureBox3.Image.Dispose(); // Libera la imagen anterior
            }

            if (pictureBox4.Image != null)
            {
                pictureBox4.Image.Dispose(); // Libera la imagen anterior
            }

            // Clona el fotograma para mostrar en los cuatro PictureBox
            Bitmap clonedFrame = (Bitmap)eventArgs.Frame.Clone();

            pictureBox1.Image = clonedFrame;
            pictureBox2.Image = (Bitmap)clonedFrame.Clone();
            pictureBox3.Image = (Bitmap)clonedFrame.Clone();
            pictureBox4.Image = (Bitmap)clonedFrame.Clone();

            // Aquí puedes realizar otras acciones con el fotograma, si es necesario
            // Por ejemplo, procesar la imagen o enviarla a otra ubicación
        }

        private void FormCamaras_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Detiene todas las cámaras cuando se cierra el formulario
            foreach (var source in videoSource)
            {
                if (source.IsRunning)
                {
                    source.SignalToStop();
                    source.WaitForStop();
                }
            }
        }
    }
}