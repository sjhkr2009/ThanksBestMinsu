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
using Newtonsoft.Json;

namespace BestMinsu_WindowApp {
    public partial class BestMinsuWindow : Form {
        private string _weightJsonPath = string.Empty;
        private List<Company> _weightJsonCompanies = null;

        public BestMinsuWindow() {
            InitializeComponent();
            UiHelper.Initialize(richTextBox1, richTextBox2);
            propertyGridWeights.SelectedObject = ScoreWeights.Current;
        }

		private void radioButton1_CheckedChanged(object sender, EventArgs e) {
			AnalysisTabController.CurrentRunMode = AnalysisTabController.RunMode.AnalysisFromMultiMode;
		}

		private void radioButton2_CheckedChanged(object sender, EventArgs e) {
			AnalysisTabController.CurrentRunMode = AnalysisTabController.RunMode.AnalysisFromSingleMode;
		}
		
		private void button1_Click(object sender, EventArgs e) {
			button1.Enabled = false;
			button2.Enabled = true;
			AnalysisTabController.Run(() => {
				button1.Invoke(new MethodInvoker(() => button1.Enabled = true));
				button2.Invoke(new MethodInvoker(() => button2.Enabled = false));
			});
		}

		private void button2_Click(object sender, EventArgs e) {
			var ret = MessageBox.Show("분석을 취소합니다. 현재까지의 분석결과는 저장되지만 나중에 재시도할 때는 처음부터 다시 분석해야 합니다.", "ㄹㅇ?", MessageBoxButtons.OKCancel);

			if (ret == DialogResult.OK) {
				AnalysisTabController.Stop();
			}
		}

		private void button3_Click(object sender, EventArgs e) {
			CompareTabController.StartCompare();
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
			CompareTabController.SetDataA(SelectFile());
		}
		
		private void buttonSelectFile2_Click(object sender, EventArgs e)
		{
			CompareTabController.SetDataB(SelectFile());
		}

		private void buttonSaveWeights_Click(object sender, EventArgs e)
		{
			ScoreWeights.Save();
			MessageBox.Show("가중치 설정이 저장되었습니다.", "저장 완료", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		private void buttonResetWeights_Click(object sender, EventArgs e)
		{
			var result = MessageBox.Show("모든 가중치를 기본값으로 복원하시겠습니까?", "기본값 복원", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
			if (result == DialogResult.OK)
			{
				ScoreWeights.Reset();
				propertyGridWeights.SelectedObject = ScoreWeights.Current;
				propertyGridWeights.Refresh();
			}
		}

		private void buttonSelectJsonForWeights_Click(object sender, EventArgs e)
		{
			string path = SelectFile();
			if (string.IsNullOrEmpty(path)) return;

			try
			{
				var json = File.ReadAllText(path);
				var companies = JsonConvert.DeserializeObject<List<Company>>(json);
				if (companies == null || companies.Count == 0)
				{
					MessageBox.Show("유효한 기업 데이터가 없습니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					return;
				}

				_weightJsonPath = path;
				_weightJsonCompanies = companies;
				buttonAnalyzeFromJson.Enabled = true;
				buttonSelectJsonForWeights.Text = $"{Path.GetFileName(path)} ({companies.Count}개 기업)";
				richTextBox3.Text = $"파일 로드 완료: {path}\n{companies.Count}개 기업 데이터\n\n가중치를 설정한 후 [이 기준으로 분석] 버튼을 눌러주세요.";
			}
			catch (Exception ex)
			{
				MessageBox.Show($"파일을 읽는 중 오류가 발생했습니다.\n{ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
				_weightJsonPath = string.Empty;
				_weightJsonCompanies = null;
				buttonAnalyzeFromJson.Enabled = false;
				buttonSelectJsonForWeights.Text = "Json 파일 선택";
			}
		}

		private void buttonAnalyzeFromJson_Click(object sender, EventArgs e)
		{
			if (_weightJsonCompanies == null || _weightJsonCompanies.Count == 0)
			{
				MessageBox.Show("먼저 Json 파일을 선택해주세요.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			try
			{
				buttonAnalyzeFromJson.Enabled = false;
				richTextBox3.Text = "분석 중...\n";

				var log = new StringBuilder();
				AnalysisHelper.Initialize(log);

				// 현재 가중치 기준으로 전체 기업 재분석
				foreach (var company in _weightJsonCompanies)
				{
					company.AnalysisAll();
				}

				// 점수 순 정렬
				_weightJsonCompanies.SortByCompanyPoint();

				// 결과 파일 저장 (바탕화면)
				SaveHelper.SaveToTextFile(_weightJsonCompanies);
				string savedPath = SaveHelper.GetPath(SaveHelper.Type.TextResult);

				// 로그에 1~30위 표시
				var sb = new StringBuilder();
				sb.AppendLine($"=== 가중치 기반 재분석 완료 ===");
				sb.AppendLine($"대상: {Path.GetFileName(_weightJsonPath)} ({_weightJsonCompanies.Count}개 기업)");
				sb.AppendLine($"결과 저장: {savedPath}");
				sb.AppendLine();
				sb.AppendLine("--- 상위 30위 ---");

				int showCount = Math.Min(30, _weightJsonCompanies.Count);
				for (int i = 0; i < showCount; i++)
				{
					var c = _weightJsonCompanies[i];
					sb.AppendLine($"[{i + 1}위] {c.CompanyName} ({c.Code:000000}) : " +
					              $"{c.TotalScore}점 ({c.RecommendPoint} - {c.WarningPoint})" +
					              (string.IsNullOrEmpty(c.Section) ? "" : $" - {c.Section}"));
				}

				richTextBox3.Text = sb.ToString();
				MessageBox.Show($"분석이 완료되었습니다.\n결과 파일: {savedPath}", "완료", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			catch (Exception ex)
			{
				richTextBox3.Text = $"분석 중 오류가 발생했습니다.\n{ex.Message}\n{ex.StackTrace}";
				MessageBox.Show($"분석 중 오류가 발생했습니다.\n{ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			finally
			{
				buttonAnalyzeFromJson.Enabled = true;
			}
		}
	}
}