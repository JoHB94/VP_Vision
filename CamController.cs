using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.IO;
using OpenCvSharp;

namespace VP_Vision
{
    internal class CamController
    {
        /*
            사진 촬영 명령, 이미지 저장
         */
        private VideoCapture capture;
        private string saveDirectory;
        //커넥션, 저장경로 초기화
        public void Initialize(ConfigurationManager configManager)
        {
            // 1. 저장 경로 설정
            saveDirectory = configManager.GetSetting("ImageSaveDirectory");
            if (!Directory.Exists(saveDirectory))
            {
                Directory.CreateDirectory(saveDirectory);
                Console.WriteLine($"Directory created at: {saveDirectory}");
            }

            // 2. 카메라 연결 초기화
            capture = new VideoCapture(0); // 기본 카메라 사용
            if (!capture.IsOpened())
            {
                throw new Exception("Failed to initialize camera. Please check the camera connection.");
            }
            Console.WriteLine("Camera initialized successfully.");
        }
        //사진 찍기
        public string CaptureImage()
        {
            if (capture == null || !capture.IsOpened())
            {
                throw new Exception("Camera is not initialized. Call InitializeConnection() first.");
            }

            using (var frame = new Mat())
            {
                capture.Read(frame); // 프레임 캡처
                if (frame.Empty())
                {
                    throw new Exception("Failed to capture image. Frame is empty.");
                }

                string timestamp = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                string filePath = Path.Combine(saveDirectory, $"{timestamp}.jpg");

                Cv2.ImWrite(filePath, frame); // 이미지 저장
                Console.WriteLine($"Image saved at: {filePath}");

                return filePath;
            }
        }
        //카메라 리소스 해제
        public void ReleaseCamera()
        {
            capture?.Release();
            Console.WriteLine("Camera released.");
        }


    }
}
