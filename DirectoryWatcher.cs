using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace VP_Vision
{
    internal class DirectoryWatcher
    {
        /*
            디렉토리를 모니터링, 새로운 이미지 감지
         */
        private string watchDirectory;      // 감시할 디렉토리
        private string processedDirectory;  // 결과 저장 디렉토리
        private FileSystemWatcher watcher;  // 파일 감시자

        public void Initialize(ConfigurationManager configManager)
        {
            // 감시 디렉토리 및 결과 디렉토리 설정
            watchDirectory = configManager.GetSetting("ImageSaveDirectory");
            processedDirectory = configManager.GetSetting("ProcessedDirectory");

            // 디렉토리가 없으면 생성
            EnsureDirectoryExists(processedDirectory);

            Console.WriteLine($"Watch Directory: {watchDirectory}");
            Console.WriteLine($"Processed Directory: {processedDirectory}");
        }

        public void StartWatching()
        {
            // FileSystemWatcher 초기화
            watcher = new FileSystemWatcher
            {
                Path = watchDirectory,
                Filter = "*.png", // png 파일만 감시
                NotifyFilter = NotifyFilters.FileName | NotifyFilters.LastWrite
            };

            // 이벤트 핸들러 등록
            watcher.Created += OnNewImageCreated;
            watcher.EnableRaisingEvents = true;

            Console.WriteLine($"Started watching directory: {watchDirectory}");
        }

        private void EnsureDirectoryExists(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
                Console.WriteLine($"Directory created: {path}");
            }
        }

        private void OnNewImageCreated(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine($"New image detected: {e.FullPath}");
            // 추후 ImageProcessor 호출 부분 추가 예정
        }

    }
}
