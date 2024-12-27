namespace VP_Vision
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //설정 파일 초기화
            ConfigurationManager configurationManager = new ConfigurationManager();
            configurationManager.LoadConfiguration();

            CamController camController = new CamController();

            try
            {
                // CamController 초기화
                camController.Initialize(configurationManager);

                // 예제: 이미지 캡처
                string imagePath = camController.CaptureImage();
                Console.WriteLine($"Captured image path: {imagePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
