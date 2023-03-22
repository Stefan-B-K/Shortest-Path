namespace SDA_0463_imd_MyProject
{
    partial class Form1
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
            cbStartVertex = new ComboBox();
            buttonDijkstra = new Button();
            cbEndVertex = new ComboBox();
            panelMap = new Panel();
            panelFrame = new Panel();
            labelOutput = new Label();
            panelFrame.SuspendLayout();
            SuspendLayout();
            // 
            // cbStartVertex
            // 
            cbStartVertex.FormattingEnabled = true;
            cbStartVertex.Location = new Point(108, 14);
            cbStartVertex.Name = "cbStartVertex";
            cbStartVertex.Size = new Size(215, 23);
            cbStartVertex.TabIndex = 0;
            cbStartVertex.Text = "  Изберете град за начало";
            // 
            // buttonDijkstra
            // 
            buttonDijkstra.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonDijkstra.Location = new Point(590, 15);
            buttonDijkstra.Name = "buttonDijkstra";
            buttonDijkstra.Size = new Size(106, 23);
            buttonDijkstra.TabIndex = 22;
            buttonDijkstra.Text = "Най-къс път";
            buttonDijkstra.UseVisualStyleBackColor = true;
            buttonDijkstra.Click += buttonDijkstra_Click;
            // 
            // cbEndVertex
            // 
            cbEndVertex.FormattingEnabled = true;
            cbEndVertex.Location = new Point(345, 15);
            cbEndVertex.Name = "cbEndVertex";
            cbEndVertex.Size = new Size(215, 23);
            cbEndVertex.TabIndex = 0;
            cbEndVertex.Text = "  Изберете град за край";
            // 
            // panelMap
            // 
            panelMap.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panelMap.BackColor = SystemColors.Window;
            panelMap.Location = new Point(14, 13);
            panelMap.Name = "panelMap";
            panelMap.Size = new Size(1132, 681);
            panelMap.TabIndex = 24;
            // 
            // panelFrame
            // 
            panelFrame.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panelFrame.BackColor = SystemColors.Window;
            panelFrame.BorderStyle = BorderStyle.Fixed3D;
            panelFrame.Controls.Add(panelMap);
            panelFrame.Location = new Point(12, 43);
            panelFrame.Name = "panelFrame";
            panelFrame.Size = new Size(1160, 706);
            panelFrame.TabIndex = 25;
            // 
            // labelOutput
            // 
            labelOutput.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            labelOutput.Location = new Point(810, 16);
            labelOutput.Name = "labelOutput";
            labelOutput.Size = new Size(268, 22);
            labelOutput.TabIndex = 26;
            labelOutput.Text = "label1";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1184, 761);
            Controls.Add(labelOutput);
            Controls.Add(panelFrame);
            Controls.Add(cbEndVertex);
            Controls.Add(cbStartVertex);
            Controls.Add(buttonDijkstra);
            MaximumSize = new Size(1200, 800);
            MinimumSize = new Size(1200, 800);
            Name = "Form1";
            Text = "Форма";
            Load += Form1_Load;
            panelFrame.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private ComboBox cbStartVertex;
        private Button buttonDijkstra;
        private ComboBox cbEndVertex;
        private Panel panelMap;
        private Panel panelFrame;
        private Label labelOutput;
    }
}