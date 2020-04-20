using System;
using System.Drawing;
using System.Windows.Forms;


namespace WinScanner
{
    using System.Diagnostics.Contracts;
    using System.Dynamic;
    using System.IO;
    using System.Text;
    using Tesseract;

    public partial class frmOCR : Form
    {
        private readonly FolderBrowserDialog folderBrowserDialog;

        public frmOCR()
        {
            InitializeComponent();
            folderBrowserDialog = new FolderBrowserDialog();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = folderBrowserDialog.SelectedPath;
                pictureBox1.ImageLocation = (fileOpenDialog.FileName);
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                //DoOCRing(openFileDialog1.FileName);
            }
        }

        private void DoOCRing(string folderPath)
        {
            using (var engine = new TesseractEngine(@"./tessdata", "eng", EngineMode.Default))
            {
                foreach (var filePath in Directory.GetFiles(folderPath, "*.png"))
                {
                    using (var img = Pix.LoadFromFile(filePath))
                    {
                        using (var page = engine.Process(img))
                        {
                            //richTextBox1.Text += page.GetMeanConfidence() + Environment.NewLine;
                            StringBuilder sb = new StringBuilder();
                            sb.Append(page.GetText());
                            richTextBox1.Text += filePath + Environment.NewLine + sb.ToString();
                            File.AppendAllText(Path.Combine(folderPath, "Speach.txt"), "*".PadRight(80, '*') + Environment.NewLine +
                                filePath + Environment.NewLine
                                + "*".PadRight(80, '*') + Environment.NewLine
                                + sb.ToString());
                            Application.DoEvents();
                        }
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

        private void ConvertPDFToImages(string filePath)
        {
            PDFHelper.ExtractImage(filePath);
        }

        private void btnPdfConvert_Click(object sender, EventArgs e)
        {
            fileOpenDialog.Filter = "PDF Files|*.pdf";
            if (fileOpenDialog.ShowDialog() == DialogResult.OK)
            {
               ConvertPDFToImages( fileOpenDialog.FileName);
            }
        }
    }
}
