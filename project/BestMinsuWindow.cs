using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BestMinsu_WindowApp {
    public partial class BestMinsuWindow : Form {
        public BestMinsuWindow() {
            InitializeComponent();
            Director.Initialize(richTextBox1);
        }

		private void radioButton1_CheckedChanged(object sender, EventArgs e) {
			Director.CurrentRunMode = Director.RunMode.AnalysisFromMultiMode;
		}

		private void radioButton2_CheckedChanged(object sender, EventArgs e) {
			Director.CurrentRunMode = Director.RunMode.AnalysisFromSingleMode;
		}
		
		private void button1_Click(object sender, EventArgs e) {
			button1.Enabled = false;
			button2.Enabled = true;
			Director.Run(() => {
				button1.Invoke(new MethodInvoker(() => button1.Enabled = true));
				button2.Invoke(new MethodInvoker(() => button2.Enabled = false));
			});
		}

		private void button2_Click(object sender, EventArgs e) {
			var ret = MessageBox.Show("분석을 취소합니다. 현재까지의 분석결과는 저장되지만 나중에 재시도할 때는 처음부터 다시 분석해야 합니다.", "ㄹㅇ?", MessageBoxButtons.OKCancel);

			if (ret == DialogResult.OK) {
				Director.Stop();
			}
		}

		private void button3_Click(object sender, EventArgs e)
		{
			
		}

		private void button4_Click(object sender, EventArgs e)
		{
			
		}
		
		private void buttonSelectFile1_Click(object sender, EventArgs e)
		{
			using (OpenFileDialog openFileDialog = new OpenFileDialog())
			{
				openFileDialog.InitialDirectory = "C:\\";
				openFileDialog.Filter = "JSON 파일 (*.json)|*.json";
				openFileDialog.FilterIndex = 1;
				if (openFileDialog.ShowDialog() == DialogResult.OK)
				{
					string filePath = openFileDialog.FileName;
					File.ReadAllText(filePath); // TODO: 파일1 -> Company로 변환 후 저장
				}
			}
		}
		
		private void buttonSelectFile2_Click(object sender, EventArgs e)
		{
			using (OpenFileDialog openFileDialog = new OpenFileDialog())
			{
				openFileDialog.InitialDirectory = "C:\\";
				openFileDialog.Filter = "JSON 파일 (*.json)|*.json";
				openFileDialog.FilterIndex = 1;
				if (openFileDialog.ShowDialog() == DialogResult.OK)
				{
					string filePath = openFileDialog.FileName;
					File.ReadAllText(filePath); // TODO: 파일2 -> Company로 변환 후 저장
				}
			}
		}
	}
}