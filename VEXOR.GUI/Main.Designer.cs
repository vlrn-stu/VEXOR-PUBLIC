namespace VEXOR.GUI
{
    partial class Main
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            codeTextBox = new FastColoredTextBoxNS.FastColoredTextBox();
            panel1 = new Panel();
            buttonWriteFile = new Button();
            buttonOpenFile = new Button();
            buttonObfuscate = new Button();
            checkBoxAntiDebug = new CheckBox();
            checkBoxStringEncryption = new CheckBox();
            checkBoxControlFlow = new CheckBox();
            labelFeatures = new Label();
            checkBoxSymbolRenaming = new CheckBox();
            ((System.ComponentModel.ISupportInitialize)codeTextBox).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // codeTextBox
            // 
            codeTextBox.AutoCompleteBracketsList = new char[]
    {
    '(',
    ')',
    '{',
    '}',
    '[',
    ']',
    '"',
    '"',
    '\'',
    '\''
    };
            codeTextBox.AutoIndentCharsPatterns = "\r\n^\\s*[\\w\\.]+(\\s\\w+)?\\s*(?<range>=)\\s*(?<range>[^;=]+);\r\n^\\s*(case|default)\\s*[^:]*(?<range>:)\\s*(?<range>[^;]+);\r\n";
            codeTextBox.AutoScrollMinSize = new Size(243, 14);
            codeTextBox.BackBrush = null;
            codeTextBox.BracketsHighlightStrategy = FastColoredTextBoxNS.BracketsHighlightStrategy.Strategy2;
            codeTextBox.CharHeight = 14;
            codeTextBox.CharWidth = 8;
            codeTextBox.DefaultMarkerSize = 8;
            codeTextBox.DisabledColor = Color.FromArgb(100, 180, 180, 180);
            codeTextBox.Dock = DockStyle.Fill;
            codeTextBox.Hotkeys = resources.GetString("codeTextBox.Hotkeys");
            codeTextBox.IsReplaceMode = false;
            codeTextBox.Language = FastColoredTextBoxNS.Language.CSharp;
            codeTextBox.LeftBracket = '(';
            codeTextBox.LeftBracket2 = '{';
            codeTextBox.Location = new Point(0, 0);
            codeTextBox.Name = "codeTextBox";
            codeTextBox.Paddings = new Padding(0);
            codeTextBox.RightBracket = ')';
            codeTextBox.RightBracket2 = '}';
            codeTextBox.SelectionColor = Color.FromArgb(60, 0, 0, 255);
            codeTextBox.ServiceColors = (FastColoredTextBoxNS.ServiceColors)resources.GetObject("codeTextBox.ServiceColors");
            codeTextBox.Size = new Size(1084, 561);
            codeTextBox.TabIndex = 0;
            codeTextBox.Text = "public static void Main(){}";
            codeTextBox.Zoom = 100;
            // 
            // panel1
            // 
            panel1.Controls.Add(buttonWriteFile);
            panel1.Controls.Add(buttonOpenFile);
            panel1.Controls.Add(buttonObfuscate);
            panel1.Controls.Add(checkBoxAntiDebug);
            panel1.Controls.Add(checkBoxStringEncryption);
            panel1.Controls.Add(checkBoxControlFlow);
            panel1.Controls.Add(labelFeatures);
            panel1.Controls.Add(checkBoxSymbolRenaming);
            panel1.Dock = DockStyle.Bottom;
            panel1.Location = new Point(0, 561);
            panel1.Name = "panel1";
            panel1.Size = new Size(1084, 100);
            panel1.TabIndex = 1;
            // 
            // buttonWriteFile
            // 
            buttonWriteFile.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonWriteFile.Location = new Point(944, 51);
            buttonWriteFile.Name = "buttonWriteFile";
            buttonWriteFile.Size = new Size(125, 44);
            buttonWriteFile.TabIndex = 7;
            buttonWriteFile.Text = "Write File";
            buttonWriteFile.UseVisualStyleBackColor = true;
            buttonWriteFile.Click += ButtonWriteFile_Click;
            // 
            // buttonOpenFile
            // 
            buttonOpenFile.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonOpenFile.Location = new Point(944, 6);
            buttonOpenFile.Name = "buttonOpenFile";
            buttonOpenFile.Size = new Size(125, 44);
            buttonOpenFile.TabIndex = 6;
            buttonOpenFile.Text = "Open File";
            buttonOpenFile.UseVisualStyleBackColor = true;
            buttonOpenFile.Click += ButtonOpenFile_Click;
            // 
            // buttonObfuscate
            // 
            buttonObfuscate.Location = new Point(241, 28);
            buttonObfuscate.Name = "buttonObfuscate";
            buttonObfuscate.Size = new Size(125, 44);
            buttonObfuscate.TabIndex = 5;
            buttonObfuscate.Text = "Obfuscate";
            buttonObfuscate.UseVisualStyleBackColor = true;
            buttonObfuscate.Click += ButtonObfuscate_Click;
            // 
            // checkBoxAntiDebug
            // 
            checkBoxAntiDebug.AutoSize = true;
            checkBoxAntiDebug.Location = new Point(141, 53);
            checkBoxAntiDebug.Name = "checkBoxAntiDebug";
            checkBoxAntiDebug.Size = new Size(87, 19);
            checkBoxAntiDebug.TabIndex = 4;
            checkBoxAntiDebug.Text = "Anti-debug";
            checkBoxAntiDebug.UseVisualStyleBackColor = true;
            // 
            // checkBoxStringEncryption
            // 
            checkBoxStringEncryption.AutoSize = true;
            checkBoxStringEncryption.Location = new Point(12, 53);
            checkBoxStringEncryption.Name = "checkBoxStringEncryption";
            checkBoxStringEncryption.Size = new Size(117, 19);
            checkBoxStringEncryption.TabIndex = 3;
            checkBoxStringEncryption.Text = "String Encryption";
            checkBoxStringEncryption.UseVisualStyleBackColor = true;
            // 
            // checkBoxControlFlow
            // 
            checkBoxControlFlow.AutoSize = true;
            checkBoxControlFlow.Location = new Point(141, 28);
            checkBoxControlFlow.Name = "checkBoxControlFlow";
            checkBoxControlFlow.Size = new Size(94, 19);
            checkBoxControlFlow.TabIndex = 2;
            checkBoxControlFlow.Text = "Control Flow";
            checkBoxControlFlow.UseVisualStyleBackColor = true;
            // 
            // labelFeatures
            // 
            labelFeatures.AutoSize = true;
            labelFeatures.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelFeatures.Location = new Point(3, 3);
            labelFeatures.Name = "labelFeatures";
            labelFeatures.Size = new Size(73, 20);
            labelFeatures.TabIndex = 1;
            labelFeatures.Text = "Features:";
            // 
            // checkBoxSymbolRenaming
            // 
            checkBoxSymbolRenaming.AutoSize = true;
            checkBoxSymbolRenaming.Location = new Point(12, 28);
            checkBoxSymbolRenaming.Name = "checkBoxSymbolRenaming";
            checkBoxSymbolRenaming.Size = new Size(123, 19);
            checkBoxSymbolRenaming.TabIndex = 0;
            checkBoxSymbolRenaming.Text = "Symbol Renaming";
            checkBoxSymbolRenaming.UseVisualStyleBackColor = true;
            // 
            // Main
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1084, 661);
            Controls.Add(codeTextBox);
            Controls.Add(panel1);
            Name = "Main";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "V3X0R";
            ((System.ComponentModel.ISupportInitialize)codeTextBox).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private FastColoredTextBoxNS.FastColoredTextBox codeTextBox;
        private Panel panel1;
        private Button buttonObfuscate;
        private CheckBox checkBoxAntiDebug;
        private CheckBox checkBoxStringEncryption;
        private CheckBox checkBoxControlFlow;
        private Label labelFeatures;
        private CheckBox checkBoxSymbolRenaming;
        private Button buttonWriteFile;
        private Button buttonOpenFile;
    }
}
