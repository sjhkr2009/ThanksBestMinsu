using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

interface AnalysisTargetData {
	public int[] TargetCodes { get; }
}

public class TargetData : AnalysisTargetData {
	public int[] TargetCodes { get; }
	public TargetData(int[] target) {
		TargetCodes = target;
	}

	
	private static string CustomFilePath => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "CompanyCodes.csv");

	private const string DefaultDataPath = "BestMinsu_WindowApp.DefaultCompanyCodes.csv";

	private static int[] _defaultCompanies = null;

	// 국내 모든 상장기업 종목 코드
	public static int[] AllListedCompanies => LoadAllCompanies();

	public static int[] LoadAllCompanies()
	{
		// csv 파일이 있으면 그걸 사용한다. 여기서 상장종목 리스트 다운로드 가능.
		// http://data.krx.co.kr/contents/MDC/MDI/mdiLoader/index.cmd?menuId=MDC0201020501

		if (File.Exists(CustomFilePath))
		{
			try
			{
				using var reader = new StreamReader(CustomFilePath);
				return GetCompanyCodes(reader);
			}
			catch (Exception ex)
			{
				MessageBox.Show("실행 파일 경로의 CSV 파일을 읽는 중 오류 발생. 기본 데이터를 사용합니다.\n" + ex.Message);
				UiHelper.ShowLog("실행 파일 경로의 CSV 파일을 읽는 중 오류 발생. 기본 데이터를 사용합니다.\n" + ex.Message);
			}
		}
		else
		{
			UiHelper.ShowLog("기본적으로 2025.03.08 기준의 모든 상장종목 데이터를 분석합니다.\n" +
				"파일 경로에 CompanyCodes.csv 이름의 상장종목코드가 있을 경우 해당 기업들을 대상으로 분석할 수 있습니다.");
		}

		return LoadDefaultCompanies();
	}
	private static int[] LoadDefaultCompanies()
	{
		if (_defaultCompanies != null)
			return _defaultCompanies;

		Assembly assembly = Assembly.GetExecutingAssembly();
		using var stream = assembly.GetManifestResourceStream(DefaultDataPath);

		if (stream == null)
		{
			MessageBox.Show("상장종목 정보를 찾을 수 없습니다.");
			return null;
		}

		using var reader = new StreamReader(stream);
		_defaultCompanies = GetCompanyCodes(reader);
		return _defaultCompanies;
	}

	private static int[] GetCompanyCodes(StreamReader dataReader)
	{
		var codes = new List<int>();
		// 첫 번째 줄은 헤더이므로 건너뜁니다.
		dataReader.ReadLine();
		while (!dataReader.EndOfStream)
		{
			string line = dataReader.ReadLine();
			if (string.IsNullOrWhiteSpace(line))
				continue;

			// CSV의 각 열은 콤마로 구분됩니다.
			string[] parts = line.Split(',');
			if (parts.Length > 0)
			{
				// 첫번째 열(종목코드)을 읽어옵니다. (따옴표 있으면 제거)
				string codeStr = parts[0].Trim().Trim('\"');
				if (int.TryParse(codeStr, out int code))
				{
					codes.Add(code);
				}
			}
		}

		return codes.ToArray();
	}
}