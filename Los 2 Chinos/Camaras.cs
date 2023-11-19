using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge.Video;
using AForge.Video.DirectShow;

namespace Los_2_Chinos
{
    public partial class Camaras : Form
    {
        public Camaras()
        {
            InitializeComponent();
            InitializeCamera();
        }
        private FilterInfoCollection videoDevices;
        private VideoCaptureDevice[] videoSource;

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
        private void FormCamaras_Load(object sender, EventArgs e)
        {
            videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);

            foreach (FilterInfo device in videoDevices)
            {
                comboBox1.Items.Add(device.Name);
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
        private void videoSource_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            pictureBox1.Image = (System.Drawing.Image)eventArgs.Frame.Clone();
        }

        private void FormCamaras_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Detiene todas las cámaras cuando se cierra el formulario
            //foreach (var source in videoSource)
            //{
              //  if (source.IsRunning)
                //{
                 //   source.SignalToStop();
                 //  source.WaitForStop();
              //  }
            //}
        }
        private void video_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            // Actualiza el PictureBox correspondiente con el fotograma actual
            if (pictureBox1.Image == null)
            {
                pictureBox1.Image = (System.Drawing.Image)eventArgs.Frame.Clone();
            }
            if (pictureBox2.Image == null)
            {
                pictureBox2.Image = (System.Drawing.Image)eventArgs.Frame.Clone();
            }
            if (pictureBox3.Image == null)
            {
                pictureBox3.Image = (System.Drawing.Image)eventArgs.Frame.Clone();
            }
            if (pictureBox4.Image == null)
            {
                pictureBox4.Image = (System.Drawing.Image)eventArgs.Frame.Clone();
            }
        }
    }
}
