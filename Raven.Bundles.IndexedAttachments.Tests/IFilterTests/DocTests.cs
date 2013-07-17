﻿using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Raven.Bundles.IndexedAttachments.Extraction;
using Raven.Imports.Newtonsoft.Json;
using Xunit;

// Tested using the Microsoft Office 2010 Filter Packs + SP1 on x64
// http://www.microsoft.com/en-us/download/details.aspx?id=17062  (x32/x64 install)
// http://www.microsoft.com/en-us/download/details.aspx?id=26606  (x32 SP1)
// http://www.microsoft.com/en-us/download/details.aspx?id=26604  (x64 SP1)

// The built-in ones that ship with Windows should also work, but may not support as many different document formats.

namespace Raven.Bundles.IndexedAttachments.Tests.IFilterTests
{
    public class DocTests
    {
        [FactIfIFilterInstalledFor(".doc")]
        public void Can_Extract_Json_From_Small_Doc()
        {
            using (var stream = File.OpenRead(@"docs\small.doc"))
            {
                var json = Extractor.GetJson(stream, ".doc");
                var str = json.ToString(Formatting.Indented);
                Debug.WriteLine("{0} characters", str.Length);
                Debug.WriteLine(str);

                const string expected = "Thundercats are on the move";
                var found = json["Text"].Values<string>().Any(x => x.IndexOf(expected, StringComparison.InvariantCultureIgnoreCase) >= 0);
                Assert.True(found);
            }
        }

        [FactIfIFilterInstalledFor(".doc")]
        public void Can_Extract_Json_From_Medium_Doc()
        {
            using (var stream = File.OpenRead(@"docs\medium.doc"))
            {
                var json = Extractor.GetJson(stream, ".doc");
                var str = json.ToString(Formatting.Indented);
                Debug.WriteLine("{0} characters", str.Length);
                Debug.WriteLine(str);

                const string expected = "We the People";
                var found = json["Text"].Values<string>().Any(x => x.IndexOf(expected, StringComparison.InvariantCultureIgnoreCase) >= 0);
                Assert.True(found);
            }
        }

        [FactIfIFilterInstalledFor(".doc")]
        public void Can_Extract_Json_From_Large_Doc()
        {
            using (var stream = File.OpenRead(@"docs\large.doc"))
            {
                var json = Extractor.GetJson(stream, ".doc");

                Debug.WriteLine("(Too big to dump without hanging visual studio.)");
                //var str = json.ToString(Formatting.Indented);
                //Debug.WriteLine("{0} characters", str.Length);
                //Debug.WriteLine(str);

                const string expected = "Holy Bible";
                var found = json["Text"].Values<string>().Any(x => x.IndexOf(expected, StringComparison.InvariantCultureIgnoreCase) >= 0);
                Assert.True(found);
            }
        }
    }
}
