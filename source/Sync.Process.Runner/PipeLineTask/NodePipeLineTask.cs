﻿using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace StoneAge.Synchronous.Process.Runner.PipeLineTask
{
    public abstract class NodePipeLineTask : ProcessPipeLineTask
    {
        private readonly string _arguments;
        private readonly string _applicationPath;

        protected NodePipeLineTask(string applicationPath, string arguments)
        {
            if (string.IsNullOrWhiteSpace(applicationPath)) throw new ArgumentException(nameof(applicationPath));
            _applicationPath = applicationPath;
            _arguments = arguments;
        }

        public override ProcessStartInfo CommandToExecute()
        {
            var fileName = "node";
            var arguments = $"\"{_applicationPath}\" {_arguments}";

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                fileName = "cmd.exe";
                arguments = $"/C node \"{_applicationPath}\" {_arguments}";
            }

            var processStartInfo = new ProcessStartInfo
            {
                FileName = fileName,
                Arguments = arguments,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                RedirectStandardInput = true,
                UseShellExecute = false,
                CreateNoWindow = true,
                StandardErrorEncoding = Encoding.UTF8,
                StandardOutputEncoding = Encoding.UTF8
            };

            return processStartInfo;
        }
    }
}
