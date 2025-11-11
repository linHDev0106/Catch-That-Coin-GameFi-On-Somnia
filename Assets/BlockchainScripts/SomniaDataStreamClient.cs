using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using UnityEngine.UI;

public class SomniaDataStreamClient : MonoBehaviour
{
    private string baseUrl = GameConfig.somniaDataStreamAPI;

    public TMP_Text yourPerformanceTextValue;
    public Text yourPerformanceTextStatus;

    // Gọi hàm này từ code khác hoặc Start() để submit
    public void SubmitScore(string wallet, int score)
    {
        StartCoroutine(PostScore(wallet, score));
    }

    private IEnumerator PostScore(string wallet, int score)
    {
        yourPerformanceTextValue.text = "Analyzing...";
        string url = $"{baseUrl}/publish";

        // Dữ liệu JSON
        string jsonData = JsonUtility.ToJson(new PlayerScore(wallet, score));
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);

        using (UnityWebRequest request = new UnityWebRequest(url, "POST"))
        {
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            Debug.Log($"📤 Sending: {jsonData}");
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("✅ Submit thành công!");
                // Sau khi POST xong, tự động GET về
                StartCoroutine(GetPlayerData(wallet));
            }
            else
            {
                Debug.LogError($"❌ Lỗi khi submit: {request.error}");
            }
        }
    }

    private IEnumerator GetPlayerData(string wallet)
    {
        string url = $"{baseUrl}/data?wallet={wallet}";
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("📥 Dữ liệu nhận về:");
                Debug.Log(request.downloadHandler.text);

                string json = request.downloadHandler.text;
                Debug.Log("📥 Full JSON: " + json);

                // Parse phần aiSummary
                AIResponse data = JsonUtility.FromJson<AIResponse>(json);
                Debug.Log("🧠 AI Summary: " + data.aiSummary);

                yourPerformanceTextValue.text = data.aiSummary;
            }
            else
            {
                Debug.LogError($"❌ Lỗi khi lấy dữ liệu: {request.error}");
                yourPerformanceTextValue.text = request.error;
            }
        }
    }

    // Struct nhỏ để tạo JSON
    [System.Serializable]
    public class PlayerScore
    {
        public string player;
        public int score;

        public PlayerScore(string wallet, int score)
        {
            this.player = wallet;
            this.score = score;
        }
    }

    [System.Serializable]
    public class AIResponse
    {
        public int totalEntries;
        public string aiSummary;
    }
}
