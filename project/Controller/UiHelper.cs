using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

public static class UiHelper {
    private static string LogContent { get; set; } = string.Empty;
    private static List<Control> LogDrawers { get; set; }

    public static void Initialize(params Control[] loggers) {
        LogDrawers = loggers.ToList();
    }

    public static void ShowLog(string msg) {
        LogContent = msg;
        UpdateLog();
    }
    
    public static void AddLog(string msg) {
        LogContent += $"\n{msg}";
        UpdateLog();
    }

    private static void UpdateLog() {
        LogDrawers?.ForEach(_logger => _logger?.Invoke(new MethodInvoker(() => _logger.Text = LogContent)));
    }
}
