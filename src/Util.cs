using System;
using System.IO;

namespace cats {
	public class Util {
		public static string FilenameToName(string filename) {
			var parts = filename.Split (new char[] {'/'});
			string last = parts [parts.Length - 1];
			parts = last.Split (new char[] { '.' });
			return parts[0];
		}

		public static string GetBasePath(string filename) {
			string fullpath = Path.GetFullPath (filename);
			return fullpath.Substring (0, fullpath.LastIndexOf ('/') + 1);
		}
	}
}
