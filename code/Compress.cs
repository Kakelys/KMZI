using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace app
{
    public static class Compress
    {
        public static int func(char[] chars) {
            char current = chars[0];
            var symbolCount = 1;
            var index = 0;

            for(int i = 1, size = chars.Length; i < size; i++)
            {
                if(current == chars[i])
                {
                    symbolCount++;
                }
                else
                {
                    chars[index++] = current;

                    if(symbolCount > 9)
                    {
                        var str = symbolCount.ToString();
                        for(var j = 0; j < str.Length; j++)
                        {
                            chars[index++] = str[j];
                        }
                    } 
                    else if(symbolCount > 1)
                    {
                        chars[index++] = (char)(symbolCount + '0');
                    } 

                    current = chars[i];
                    symbolCount = 1;
                }
            }

            chars[index++] = current;

            if(symbolCount > 9)
            {
                var str = symbolCount.ToString();
                for(var j = 0; j < str.Length; j++)
                {
                    chars[index++] = str[j];
                }
            } 
            else if(symbolCount > 1)
            {
                chars[index++] = (char)(symbolCount+'0');
            } 

            return index;
        }
    }
}