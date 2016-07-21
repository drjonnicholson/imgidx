// Copyright 2016 Dr Jon Nicholson
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace imgidx
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "ImgIdx: Image index generator";

            string current = Directory.GetCurrentDirectory();

            Console.WriteLine("Processing: {0}", current);

            SearchOption opt = SearchOption.TopDirectoryOnly;
            IEnumerable<string> paths = Directory.GetFiles(current, "*.*", opt).Where(s => isImg(s));

            List<string> lines = new List<string>();
            List<string> errors = new List<string>();
            lines.Add("Index,Description");
            int failed = 0;
            int passed = 0;

            foreach (string path in paths)
            {
                try
                {
                    // Strip the path
                    string filename = path.Substring(current.Length + 1);
                    
                    // Get the index number
                    int space = filename.IndexOf(' ');
                    if(space < 0)
                    {
                        throw new FormatException("Couldn't find index, check file name for pattern '[index] [description].[ext]'");
                    }
                    string index = filename.Substring(0, space);

                    // Get the rest of the filename without extension
                    int length = filename.LastIndexOf('.') - space - 1;
                    string description = filename.Substring(space + 1, length);

                    // Add a line to the CSV
                    lines.Add(index + "," + description);
                    passed++;
                }
                catch (Exception e)
                {
                    errors.Add("File couldn't be processed: " + path + "\n" + e.Message + "\n" + e.StackTrace + "\n");
                    failed++;
                }
            }

            write(current, @"\index.csv", lines);
            Console.WriteLine("Processed {0} files successfully", passed);

            write(current, @"\index.log", errors);
            if (failed > 0)
            {
                Console.Error.WriteLine("{0} files failed, see index.log for details.", failed);
            }

            Console.WriteLine("Done, press any key to exit.");
            Console.ReadKey();
        }

        static Boolean isImg(string path)
        {
            return path.EndsWith(".jpg", StringComparison.CurrentCultureIgnoreCase)
                || path.EndsWith(".jpeg", StringComparison.CurrentCultureIgnoreCase)
                || path.EndsWith(".png", StringComparison.CurrentCultureIgnoreCase)
                || path.EndsWith(".gif", StringComparison.CurrentCultureIgnoreCase)
                || path.EndsWith(".tiff", StringComparison.CurrentCultureIgnoreCase);
        }

        static void write(string current, string file, List<string> lines)
        {
            using (StreamWriter index = new StreamWriter(current + file))
            {
                foreach (string line in lines)
                {
                    index.WriteLine(line);
                }
            }
        }
    }
}
