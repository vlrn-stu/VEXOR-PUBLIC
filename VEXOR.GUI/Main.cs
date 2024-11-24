using VEXOR.Obfuscation;

namespace VEXOR.GUI
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void ButtonObfuscate_Click(object sender, EventArgs e)
        {
            var obfuscator = new Obfuscator(codeTextBox.Text);
            codeTextBox.Text = obfuscator.Obfuscate(
                checkBoxSymbolRenaming.Checked,
                checkBoxControlFlow.Checked,
                checkBoxStringEncryption.Checked,
                checkBoxAntiDebug.Checked);
        }

        private void ButtonOpenFile_Click(object sender, EventArgs e)
        {
            using OpenFileDialog openFileDialog = new();
            openFileDialog.Filter = "C# Files (*.cs)|*.cs";
            openFileDialog.Title = "Open C# Source File";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string fileContent = File.ReadAllText(openFileDialog.FileName);
                    codeTextBox.Text = fileContent;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error opening file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ButtonWriteFile_Click(object sender, EventArgs e)
        {
            using SaveFileDialog saveFileDialog = new();
            saveFileDialog.Filter = "C# Files (*.cs)|*.cs";
            saveFileDialog.Title = "Save Obfuscated C# File";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    File.WriteAllText(saveFileDialog.FileName, codeTextBox.Text);
                    MessageBox.Show("File saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error saving file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}