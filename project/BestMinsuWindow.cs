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
            UiHelper.Initialize(richTextBox1, richTextBox2);
        }

		private void radioButton1_CheckedChanged(object sender, EventArgs e) {
			AnalysisTabDirector.CurrentRunMode = AnalysisTabDirector.RunMode.AnalysisFromMultiMode;
		}

		private void radioButton2_CheckedChanged(object sender, EventArgs e) {
			AnalysisTabDirector.CurrentRunMode = AnalysisTabDirector.RunMode.AnalysisFromSingleMode;
		}
		
		private void button1_Click(object sender, EventArgs e) {
			button1.Enabled = false;
			button2.Enabled = true;
			AnalysisTabDirector.Run(() => {
				button1.Invoke(new MethodInvoker(() => button1.Enabled = true));
				button2.Invoke(new MethodInvoker(() => button2.Enabled = false));
			});
		}

		private void button2_Click(object sender, EventArgs e) {
			var ret = MessageBox.Show("분석을 취소합니다. 현재까지의 분석결과는 저장되지만 나중에 재시도할 때는 처음부터 다시 분석해야 합니다.", "ㄹㅇ?", MessageBoxButtons.OKCancel);

			if (ret == DialogResult.OK) {
				AnalysisTabDirector.Stop();
			}
		}

		private void button3_Click(object sender, EventArgs e) {
			CompareTabDirector.StartCompare();
		}

		private void button4_Click(object sender, EventArgs e) {
			// TODO: 결과 파일 저장
		}
		
		private static string DefaultUsedDirectory { get; }= Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
		private string LastUsedDirectory = DefaultUsedDirectory; // 기본값

		private string SelectFile() {
			using (OpenFileDialog openFileDialog = new OpenFileDialog())
			{
				if (Directory.Exists(LastUsedDirectory))
					openFileDialog.InitialDirectory = LastUsedDirectory;
				else
					openFileDialog.InitialDirectory = DefaultUsedDirectory;

				openFileDialog.Filter = "JSON 파일 (*.json)|*.json";
				openFileDialog.FilterIndex = 1;
				if (openFileDialog.ShowDialog() == DialogResult.OK)
				{
					LastUsedDirectory = Path.GetDirectoryName(openFileDialog.FileName);
					return openFileDialog.FileName;
				}
			}

			return string.Empty;
		} 
		
		private void buttonSelectFile1_Click(object sender, EventArgs e)
		{
			CompareTabDirector.SetDataA(SelectFile());
		}
		
		private void buttonSelectFile2_Click(object sender, EventArgs e)
		{
			CompareTabDirector.SetDataB(SelectFile());
		}
	}
}