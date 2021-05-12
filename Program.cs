﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace findlog
{
	class Program
	{
		static void Main(string[] args)
		{
			if (args.Length < 2 || args.Length > 3)
			{
				Console.WriteLine(@"The parameters are unexpected.

Usage: findlog [path] [pattern] [rowoffset:int?]");
				return;
			}
			string path = Path.GetDirectoryName(args[0]);
			DirectoryInfo dir = new DirectoryInfo(path);
			if (!dir.Exists)
			{
				Console.WriteLine("Directory not exists." + path);
				return;
			}

			Regex regex = new Regex(args[1]);
			int offset = args.Length == 3 ? int.Parse(args[2]) : 0;

			string fileSearch = Path.GetFileName(args[0]);

			var files = string.IsNullOrEmpty(fileSearch) ? dir.GetFiles() : dir.GetFiles(fileSearch);

			foreach (var file in files)
			{
				Queue<int> offsetCounters = new Queue<int>();
				Queue<string> pastLines = new Queue<string>();
				int lineCount = 0;
				using (var reader = new StreamReader(file.FullName))
				{
					while (!reader.EndOfStream)
					{
						string line = reader.ReadLine();

						if (offsetCounters.Count > 0 && offsetCounters.Peek() == lineCount)
						{
							Console.WriteLine(line);
							offsetCounters.Dequeue();
						}

						if (regex.IsMatch(line))
						{
							if (offset == 0)
							{
								Console.WriteLine(line);
							}
							else if (offset > 0)
							{
								offsetCounters.Enqueue(lineCount + offset);
							}
							else // offset < 0
							{
								Console.WriteLine(pastLines.Peek());
							}
						}

						if (offset < 0)
						{
							pastLines.Enqueue(line);
							if (pastLines.Count > -offset)
								pastLines.Dequeue();
						}

						lineCount++;
					}
				}
			}
		}
	}
}