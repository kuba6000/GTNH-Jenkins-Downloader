﻿using System;
using System.IO;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;

namespace GTNHJenksinsDownloader
{
    static class Settings
    {
        static public readonly string appdirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "GTNHJenksinsDownloader\\");
        static public readonly string settingspath = Path.Combine(appdirectory, "settings.conf");
        static public readonly string apppath = Path.Combine(appdirectory, "GTNHJenksinsDownloader.exe");
        static public readonly string appname = "GTNH Jenksins Downloader";
        public class Options {
            public bool quitonclose = false;
            public bool autoupdates = false;
            public AutoUpdateSelector autoupdateinterval = AutoUpdateSelector.onstartup;
            // autostart is dynamic
            public bool startintray = false;
            public string modpath = "";
            public string jenkinspath = "http://jenkins.usrv.eu:8080/";
            public bool client = true;
            public bool server = false;
            public string servermodpath = "";
            public List<string> blacklist = new List<string>();
            public bool nevershowtrayhide = false;
        }
        static public Options options = new Options();
        static public void Load()
        {
            if (!File.Exists(settingspath))
                return;
            dynamic parsed = JsonConvert.DeserializeObject(File.ReadAllText(settingspath));
            try
            {
                options.quitonclose = parsed.quitonclose;
                options.autoupdates = parsed.autoupdates;
                options.autoupdateinterval = parsed.autoupdateinterval;
                options.startintray = parsed.startintray;
                options.modpath = parsed.modpath;
                options.jenkinspath = parsed.jenkinspath;
                options.client = parsed.client;
                options.server = parsed.server;
                options.servermodpath = parsed.servermodpath;
                Newtonsoft.Json.Linq.JArray blacklist = parsed.blacklist;
                if(blacklist != null)
                    foreach (string item in blacklist)
                        options.blacklist.Add(item);
                options.nevershowtrayhide = parsed.nevershowtrayhide;
            }
            catch { }
        }
        static public void Save()
        {
            File.WriteAllText(settingspath, JsonConvert.SerializeObject(options));
        }
    }
    enum AutoUpdateSelector : int
    {
        onstartup = 0,
        everyhour
    }
}
