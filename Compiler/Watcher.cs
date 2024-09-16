using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Watchers
{
    public class Watcher
    {
        private readonly TimeSpan               _throttle;
        public readonly  HashSet<string>        Extensions   = new HashSet<string>();
        public readonly  HashSet<DirectoryInfo> Directories  = new HashSet<DirectoryInfo>();
        public readonly  HashSet<string>        ChangedPaths = new HashSet<string>();

        public bool FilesOnly       { get; private set; }
        public bool IncludeSymlinks { get; private set; }

        private List<FileSystemWatcher> _watchers = new List<FileSystemWatcher>();
        private DateTime                _lastTriggered;

        public Watcher(string directory, string extension, int throttleMs) :
            this(new[] {new DirectoryInfo(directory)}, new[] {extension}, TimeSpan.FromMilliseconds(throttleMs)){}

        public Watcher(IEnumerable<DirectoryInfo> directories, string[] extensions, TimeSpan throttle, bool includeSymlinks = true, bool filesOnly = true)
        {
            IncludeSymlinks = includeSymlinks;
            FilesOnly = filesOnly;
            
            _throttle = throttle;
            foreach(var directoryInfo in directories)
            {
                Collect(directoryInfo);
            }

            void Collect(DirectoryInfo directoryInfo)
            {
                Directories.Add(directoryInfo);
                foreach(var subdir in directoryInfo.GetDirectories("*", SearchOption.AllDirectories))
                {
                    if(IncludeSymlinks && (subdir.Attributes & FileAttributes.ReparsePoint) != 0)
                    {
                        Collect(subdir);
                    }
                }
            }

            foreach(string extension in extensions)
            {
                Extensions.Add(extension);
            }
        }

        public void Start()
        {
            foreach(var directory in Directories)
            {
                foreach(var extension in Extensions)
                {
                    var pattern = extension;
                    if (!pattern.StartsWith("*.")) pattern = "*." + pattern;
                    
                    var watcher = new FileSystemWatcher(directory.FullName, pattern);
                    watcher.IncludeSubdirectories = true;
                    watcher.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.Size;
                    //watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.Size | NotifyFilters.CreationTime;
                    watcher.Created += WatcherChanged;
                    watcher.Changed += WatcherChanged;
                    watcher.Deleted += WatcherChanged;
                    watcher.Renamed += WatcherChanged;
                    _watchers.Add(watcher);
                }
            }

            foreach(var watcher in _watchers)
            {
                watcher.EnableRaisingEvents = true;
            }
        }

        private void WatcherChanged(object sender, FileSystemEventArgs e)
        {
            if(FilesOnly && Directory.Exists(e.FullPath)) return;
            ChangedPaths.Add(e.FullPath);
            _lastTriggered = DateTime.Now;
        }

        public bool DumpChanged(out List<string> paths)
        {
            paths = null;
            if(ChangedPaths.Count == 0 || DateTime.Now - _lastTriggered < _throttle) return false;

            paths = ChangedPaths.ToList();
            ChangedPaths.Clear();
            return true;
        }

        public void Stop()
        {
            foreach(var watcher in _watchers)
            {
                watcher.EnableRaisingEvents = false;
                watcher.Dispose();
            }
            
            _watchers.Clear();
        }
    }
}