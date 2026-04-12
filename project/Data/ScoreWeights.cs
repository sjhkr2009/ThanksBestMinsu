using System;
using System.ComponentModel;
using System.IO;
using Newtonsoft.Json;

[Serializable]
public class ScoreWeights {
    private static ScoreWeights _current;
    public static ScoreWeights Current {
        get {
            if (_current == null) _current = Load();
            return _current;
        }
        set => _current = value;
    }

    private static string SavePath => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ScoreWeights.json");
    
    // 1. 시가총액
    [Category("1. 시가총액"), DisplayName("정보 없음 감점"), Description("시가총액 정보가 없을 때 부여할 감점")]
    public int NoMarketCapPenalty { get; set; } = 1000;

    [Category("1. 시가총액"), DisplayName("저시가총액 기준 (억)"), Description("이 금액 미만이면 감점 (루트(기준-시가총액)만큼 감점)")]
    public int LowMarketCapThreshold { get; set; } = 1000;

    [Category("1. 시가총액"), DisplayName("고시가총액 기준 (억)"), Description("이 금액 초과면 가점 (예: 10000 = 1조)")]
    public int HighMarketCapThreshold { get; set; } = 10000;

    [Category("1. 시가총액"), DisplayName("고시가총액 가점"), Description("시가총액이 높을 때 부여할 가점")]
    public int HighMarketCapRecommend { get; set; } = 10;
    
    // 2. PER
    [Category("2. PER"), DisplayName("PER 정보 없음 감점"), Description("PER 정보가 없을 때(당기순손실 예상) 감점")]
    public int NoPerPenalty { get; set; } = 20;

    [Category("2. PER"), DisplayName("동종업계 PER 하한"), Description("동종업계 PER이 이 값 미만이면 절대평가로 전환")]
    public float SimilarPerLowBound { get; set; } = 1f;

    [Category("2. PER"), DisplayName("동종업계 PER 상한"), Description("동종업계 PER이 이 값 초과면 절대평가로 전환")]
    public float SimilarPerHighBound { get; set; } = 30f;
    
    [Category("2-1. PER 상대평가"), DisplayName("계수"), Description("동종업계 PER과의 차이율에 곱할 계수 (10% 차이마다 이 값의 1/10점)")]
    public float PerRelativeCoefficient { get; set; } = 10f;

    [Category("2-1. PER 상대평가"), DisplayName("최대 점수")]
    public float PerRelativeMax { get; set; } = 30f;
    
    [Category("2-2. PER 절대평가"), DisplayName("고PER 기준"), Description("이 값 초과면 감점")]
    public float PerAbsoluteHighThreshold { get; set; } = 30f;

    [Category("2-2. PER 절대평가"), DisplayName("고PER 계수")]
    public float PerAbsoluteHighCoefficient { get; set; } = 20f;

    [Category("2-2. PER 절대평가"), DisplayName("고PER 최대 감점")]
    public float PerAbsoluteHighMax { get; set; } = 30f;

    [Category("2-2. PER 절대평가"), DisplayName("고PER 최소 감점")]
    public float PerAbsoluteHighMin { get; set; } = 10f;

    [Category("2-2. PER 절대평가"), DisplayName("저PER 기준"), Description("이 값 미만이면 가점")]
    public float PerAbsoluteLowThreshold { get; set; } = 10f;

    [Category("2-2. PER 절대평가"), DisplayName("저PER 배율"), Description("(기준 - PER) × 배율 = 가점")]
    public float PerAbsoluteLowMultiplier { get; set; } = 5f;

    [Category("2-2. PER 절대평가"), DisplayName("저PER 최소 가점")]
    public int PerAbsoluteLowMin { get; set; } = 1;

    [Category("2-2. PER 절대평가"), DisplayName("저PER 최대 가점")]
    public int PerAbsoluteLowMax { get; set; } = 30;
    
    // 3. 미래 PER
    [Category("3-1. 미래 PER 상대평가"), DisplayName("계수"), Description("현재 PER과의 차이율에 곱할 계수")]
    public float ExpPerRelativeCoefficient { get; set; } = 10f;

    [Category("3-1. 미래 PER 상대평가"), DisplayName("최대 점수")]
    public float ExpPerRelativeMax { get; set; } = 10f;
    
    [Category("3-2. 미래 PER 절대평가"), DisplayName("고PER 기준"), Description("이 값 초과면 감점")]
    public float ExpPerAbsoluteHighThreshold { get; set; } = 30f;

    [Category("3-2. 미래 PER 절대평가"), DisplayName("고PER 계수")]
    public float ExpPerAbsoluteHighCoefficient { get; set; } = 20f;

    [Category("3-2. 미래 PER 절대평가"), DisplayName("고PER 최대 감점")]
    public float ExpPerAbsoluteHighMax { get; set; } = 10f;

    [Category("3-2. 미래 PER 절대평가"), DisplayName("저PER 기준"), Description("이 값 미만이면 가점")]
    public float ExpPerAbsoluteLowThreshold { get; set; } = 10f;

    [Category("3-2. 미래 PER 절대평가"), DisplayName("저PER 배율"), Description("(기준 - 예상PER) × 배율 = 가점")]
    public float ExpPerAbsoluteLowMultiplier { get; set; } = 5f;

    [Category("3-2. 미래 PER 절대평가"), DisplayName("저PER 최소 가점")]
    public int ExpPerAbsoluteLowMin { get; set; } = 1;

    [Category("3-2. 미래 PER 절대평가"), DisplayName("저PER 최대 가점")]
    public int ExpPerAbsoluteLowMax { get; set; } = 10;
    
    // 4. PBR
    [Category("4. PBR"), DisplayName("PBR 정보 없음 감점")]
    public int NoPbrPenalty { get; set; } = 10;

    [Category("4. PBR"), DisplayName("고PBR 기준 (배)"), Description("이 값 초과면 감점")]
    public float PbrHighThreshold { get; set; } = 3f;

    [Category("4. PBR"), DisplayName("저PBR 기준 (배)"), Description("이 값 미만이면 가점")]
    public float PbrLowThreshold { get; set; } = 1f;

    [Category("4. PBR"), DisplayName("계수"), Description("PBR 차이율에 곱할 계수")]
    public float PbrCoefficient { get; set; } = 5f;

    [Category("4. PBR"), DisplayName("최대 점수")]
    public float PbrMax { get; set; } = 10f;
    
    // 5. 시가배당률
    [Category("5. 시가배당률"), DisplayName("배당률 기준 (%)"), Description("이 값 초과면 가점")]
    public float DividendThreshold { get; set; } = 2f;

    [Category("5. 시가배당률"), DisplayName("계수")]
    public float DividendCoefficient { get; set; } = 10f;

    [Category("5. 시가배당률"), DisplayName("최대 가점")]
    public float DividendMax { get; set; } = 20f;

    [Category("5. 시가배당률"), DisplayName("최소 가점")]
    public float DividendMin { get; set; } = 10f;
    
    // 6. 연간 실적
    [Category("6. 연간 실적"), DisplayName("당기순손실 기본 감점"), Description("연도별로 이 값 × (연도순서)만큼 감점. 최근일수록 큰 감점.")]
    public int YearNetLossBasePoint { get; set; } = 10;

    [Category("6. 연간 실적"), DisplayName("영업손실 기본 감점"), Description("연도별로 이 값 × (연도순서)만큼 감점")]
    public int YearGrossLossBasePoint { get; set; } = 10;

    [Category("6. 연간 실적"), DisplayName("예상 영업이익 없음 감점"), Description("올해 예상 영업이익이 없을 때 감점")]
    public int NoExpectedGrossProfitPenalty { get; set; } = 20;

    [Category("6. 연간 실적"), DisplayName("영업이익 증가 최소 가점"), Description("영업이익 증가 시 최소로 부여할 가점")]
    public int GrossProfitIncreaseMin { get; set; } = 10;

    [Category("6. 연간 실적"), DisplayName("영업이익 증가 최대 기본값"), Description("최대 가점 = 이 값 × (1 + 연도 인덱스)")]
    public int GrossProfitIncreaseMaxBase { get; set; } = 10;

    [Category("6. 연간 실적"), DisplayName("흑자전환 가점"), Description("전년 영업손실에서 흑자로 전환했을 때 가점")]
    public int TurnaroundRecommend { get; set; } = 10;

    [Category("6. 연간 실적"), DisplayName("영업이익 감소 배율"), Description("감소율(%)에 이 배율을 곱하여 감점 산출")]
    public float GrossProfitDecreaseMultiplier { get; set; } = 0.5f;

    [Category("6. 연간 실적"), DisplayName("영업이익 감소 최대 기본값"), Description("최대 감점 = 이 값 × (1 + 연도 인덱스)")]
    public int GrossProfitDecreaseMaxBase { get; set; } = 10;

    [Category("6. 연간 실적"), DisplayName("PER 평균비교 계수"), Description("최근 PER이 3년 평균보다 낮을 때 적용할 계수")]
    public float PerAvgCoefficient { get; set; } = 20f;

    [Category("6. 연간 실적"), DisplayName("PER 평균비교 최대 가점")]
    public float PerAvgMax { get; set; } = 20f;

    [Category("6. 연간 실적"), DisplayName("PER 평균비교 최소 가점")]
    public float PerAvgMin { get; set; } = 5f;
    
    // 7. 재무 안정성
    [Category("7. 재무 안정성"), DisplayName("부채비율 기준 (%)"), Description("이 값 초과면 감점")]
    public float DebtRatioThreshold { get; set; } = 100f;

    [Category("7. 재무 안정성"), DisplayName("부채비율 감점")]
    public int DebtRatioPenalty { get; set; } = 10;

    [Category("7. 재무 안정성"), DisplayName("당좌비율 하한 기준 (%)"), Description("이 값 미만이면 감점")]
    public float QuickRatioLowThreshold { get; set; } = 100f;

    [Category("7. 재무 안정성"), DisplayName("당좌비율 하한 감점")]
    public int QuickRatioLowPenalty { get; set; } = 10;

    [Category("7. 재무 안정성"), DisplayName("당좌비율 상한 기준 (%)"), Description("이 값 초과면 가점")]
    public float QuickRatioHighThreshold { get; set; } = 200f;

    [Category("7. 재무 안정성"), DisplayName("당좌비율 상한 가점")]
    public int QuickRatioHighRecommend { get; set; } = 10;

    [Category("7. 재무 안정성"), DisplayName("유보율 하한 기준 (%)"), Description("이 값 미만이면 감점")]
    public float ReserveRatioLowThreshold { get; set; } = 500f;

    [Category("7. 재무 안정성"), DisplayName("유보율 하한 감점")]
    public int ReserveRatioLowPenalty { get; set; } = 5;

    [Category("7. 재무 안정성"), DisplayName("유보율 상한 기준 (%)"), Description("이 값 초과면 가점")]
    public float ReserveRatioHighThreshold { get; set; } = 2000f;

    [Category("7. 재무 안정성"), DisplayName("유보율 상한 가점")]
    public int ReserveRatioHighRecommend { get; set; } = 5;
    
    // 8. 분기 실적
    [Category("8. 분기 실적"), DisplayName("당기순손실 감점"), Description("분기별 당기순손실 시 감점")]
    public int QuarterNetLossPenalty { get; set; } = 10;
    
    public static ScoreWeights Load() {
        try {
            if (File.Exists(SavePath)) {
                var json = File.ReadAllText(SavePath);
                var loaded = JsonConvert.DeserializeObject<ScoreWeights>(json);
                if (loaded != null)
                    return loaded;
            }
        } catch (Exception) { /* 읽기 실패 시 기본값 사용 */ }
        return new ScoreWeights();
    }

    public static void Save() {
        try {
            var json = JsonConvert.SerializeObject(Current, Formatting.Indented);
            File.WriteAllText(SavePath, json);
        } catch (Exception) { /* Ignore */ }
    }

    public static void Reset() {
        _current = new ScoreWeights();
    }
}

