using System;
using System.Drawing;
using System.Windows.Forms;


namespace WinScanner
{
    using System.Diagnostics.Contracts;
    using System.Dynamic;
    using Tesseract;

    public partial class frmOCR : Form
    {
        public frmOCR()
        {
            InitializeComponent();
        }

        private void frmOCR_Load(object sender, EventArgs e)
        {
            
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {

                textBox1.Text = openFileDialog1.FileName;
                pictureBox1.ImageLocation = (openFileDialog1.FileName);
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                //DoOCRing(openFileDialog1.FileName);
            }
        }

        private void DoOCRing(string filePath)
        {
            using (var engine = new TesseractEngine(@"./tessdata", "eng", EngineMode.Default))
            {
                using (var img = Pix.LoadFromFile(filePath))
                {
                    using (var page = engine.Process(img))
                    {
                        richTextBox1.Text += page.GetMeanConfidence() + Environment.NewLine;
                        richTextBox1.Text += Environment.NewLine + page.GetText();
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            UseWaitCursor = true;
            btnBrowse.Enabled = button1.Enabled = false;
            DoOCRing(textBox1.Text);
            MessageBox.Show("Scan completed.", "OCR Scan", MessageBoxButtons.OK, MessageBoxIcon.Information);
            UseWaitCursor = false;
            btnBrowse.Enabled = button1.Enabled = true;
        }
    }
}
