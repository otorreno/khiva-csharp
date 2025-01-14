﻿using khiva.library;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace khiva.library.Tests
{
    [TestFixture]
    public class LibraryTests
    {

        [Test]
        public void PrintBackendInfoTest()
        {
            string[] info_splitted;
            using (StringWriter writer = new StringWriter())
            {
                Console.SetOut(writer);
                Library.PrintBackendInfo();
                string info = writer.ToString();
                info_splitted = info.Split(' ');
            }
            StreamWriter standardOutput = new StreamWriter(Console.OpenStandardOutput());
            standardOutput.AutoFlush = true;
            Console.SetOut(standardOutput);
            Assert.AreEqual("ArrayFire", info_splitted[0]);
        }

        [Test]
        public void GetBackendInfoTest()
        {
            String backend_info = Library.GetBackendInfo();
            string[] info_split = backend_info.Split(' ');
            Assert.AreEqual("ArrayFire", info_split[0]);
        }

        [Test]
        public void SetKhivaBackendTest()
        {
            int backends = Library.GetKhivaBackends();
            int cuda = backends & (int)Library.Backend.KHIVA_BACKEND_CUDA;
            int opencl = backends & (int)Library.Backend.KHIVA_BACKEND_OPENCL;
            int cpu = backends & (int)Library.Backend.KHIVA_BACKEND_CPU;

            if (cuda != 0)
            {
                Library.SetKhivaBackend(Library.Backend.KHIVA_BACKEND_CUDA);
                Assert.AreEqual(Library.Backend.KHIVA_BACKEND_CUDA, Library.GetKhivaBackend());
            }

            if (opencl != 0)
            {
                Library.SetKhivaBackend(Library.Backend.KHIVA_BACKEND_OPENCL);
                Assert.AreEqual(Library.Backend.KHIVA_BACKEND_OPENCL, Library.GetKhivaBackend());
            }

            if (cpu != 0)
            {
                Library.SetKhivaBackend(Library.Backend.KHIVA_BACKEND_CPU);
                Assert.AreEqual(Library.Backend.KHIVA_BACKEND_CPU, Library.GetKhivaBackend());
            }
        }



        [Test]
        public void GetDeviceIDTest()
        {
            int backends = Library.GetKhivaBackends();
            int cuda = backends & (int)Library.Backend.KHIVA_BACKEND_CUDA;
            int opencl = backends & (int)Library.Backend.KHIVA_BACKEND_OPENCL;
            int cpu = backends & (int)Library.Backend.KHIVA_BACKEND_CPU;

            if (cuda != 0)
            {
                Library.SetKhivaBackend(Library.Backend.KHIVA_BACKEND_CUDA);
                for (int i = 0; i < Library.GetKhivaDeviceCount(); i++)
                {
                    Library.SetKhivaDevice(i);
                    Assert.AreEqual(i, Library.GetKhivaDeviceID());
                }
            }

            if (opencl != 0)
            {
                Library.SetKhivaBackend(Library.Backend.KHIVA_BACKEND_OPENCL);
                for (int i = 0; i < Library.GetKhivaDeviceCount(); i++)
                {
                    Library.SetKhivaDevice(i);
                    Assert.AreEqual(i, Library.GetKhivaDeviceID());
                }
            }

            if (cpu != 0)
            {
                Library.SetKhivaBackend(Library.Backend.KHIVA_BACKEND_CPU);
                for (int i = 0; i < Library.GetKhivaDeviceCount(); i++)
                {
                    Library.SetKhivaDevice(i);
                    Assert.AreEqual(i, Library.GetKhivaDeviceID());
                }
            };
        }

        [Test]
        public void GetKhivaVersionTest()
        {
            Assert.AreEqual(GetKhivaVersionFromFile(), Library.GetKhivaVersion());
        }

        private String GetKhivaVersionFromFile()
        {
            String version = "";
            String filePath;
            String Os = System.Environment.OSVersion.Platform.ToString().ToLower();

            if (Os.Contains("win"))
            {
                filePath = "C:/Program Files/Khiva/v0/include/khiva/version.h";
            }
            else
            {
                filePath = "/usr/local/include/khiva/version.h";
            }

            String data = "";

            try
            {
                data = File.ReadAllText(filePath);
            }
            catch (IOException e)
            {
                Console.WriteLine(e.ToString());
            }

            MatchCollection matches = Regex.Matches(data, "([0-9]+\\.[0-9]+\\.[0-9]+)");

            if (matches.Count != 0)
            {
                version = matches[0].Groups[1].Value;
            }

            return version;

        }
    }
}