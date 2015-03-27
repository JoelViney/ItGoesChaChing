using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItGoesChaChing.Helpers
{
	public static class StringHelper
	{
		/// <summary>
		/// Spaces a string from TitleCase to 'Title Case'.
		/// </summary>
		public static string SpaceTitleCase(string str)
		{
			char[] charArray = str.ToCharArray();
			string result = "";

			bool previousWasUppercase = false;
			foreach(char ch in charArray)
			{
				if (System.Char.IsUpper(ch))
				{
					if (!previousWasUppercase)
						result += " " + ch;
					else
						result += ch;

					previousWasUppercase = true;
				}
				else
				{
					result += ch;
					previousWasUppercase = false;
				}
			}
			result = result.TrimStart();
			
			return result;
		}

	}
}
