using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Aveva.PDMS.PMLNet;

namespace pmlRegex
{
    [PMLNetCallable()]
    public class Regularexpression
    {
        bool IgnoreCase = true;
        [PMLNetCallable()]
        public Regularexpression() { }
        [PMLNetCallable()]
        public Regularexpression(bool ignoreCase) 
        {
            IgnoreCase = ignoreCase;
        }
        [PMLNetCallable()]
        public void Assign(Regularexpression that)
        {

        }
        [PMLNetCallable()]
        public bool IsMatch(string source, string pattern) 
        {
            Regex regex = Init(pattern);
            return regex.IsMatch(source);
        }
        [PMLNetCallable()]
        public string Replace(string source, string pattern, string replacement)
        {
            Regex regex = Init(pattern);
            return regex.Replace(source, replacement);
        }
        [PMLNetCallable()]
        public Hashtable Replace(Hashtable source, string pattern, string replacement)
        {
            List<string> table = source.Values.Cast<string>().Select(a => Replace(a.ToString(), pattern, replacement)).ToList();
            Hashtable result = new Hashtable();
            int i = 0;
            foreach(string value in table)
            {
                i++;result.Add(Convert.ToDouble(i), value);
            }
            return result;
        }
        [PMLNetCallable()]
        public string Remove(string source, string pattern)
        {
            return Replace(source, pattern, "");
        }
        [PMLNetCallable()]
        public Hashtable Remove(Hashtable source, string pattern)
        {
            return Replace(source, pattern, "");
        }
        [PMLNetCallable()]
        public Hashtable MatchedPatterns(string source, string pattern)
        {
            Regex regex = Init(pattern);
            List< Match> aa = regex.Matches(source).Cast<Match>().Where(mat=>mat.Value!="").ToList();
            Hashtable result = new Hashtable();
            int i = 0;
            foreach (Match value in aa)
            {
                i++; result.Add(Convert.ToDouble(i), value.Value);
            }
            return result;
        }
        [PMLNetCallable()]
        public double Match(string source , string pattern)
        {
            Regex regex = Init(pattern);
            return regex.Matches(source).Count;
        }
        [PMLNetCallable()]
        public Hashtable MatchedArrayIndices(Hashtable hashtable, string pattern)
        {
            Regex regex = Init(pattern);
            List<double> indextable= hashtable.Values.Cast<string>().Where(a => regex.IsMatch(a)).Select((a,index)=>Convert.ToDouble( index + 1)).ToList();
            Hashtable result = new Hashtable();
            int i = 0;
            foreach (double value in indextable)
            {
                i++; result.Add(Convert.ToDouble(i), value);
            }
            return result;
        }
        [PMLNetCallable()]
        public Hashtable MatchedArray(Hashtable hashtable, string pattern)
        {
            Regex regex = Init(pattern);
            List<string> table = hashtable.Values.Cast<string>().ToList().Where(a => regex.IsMatch(a)).Select(a => a.ToString()).ToList();
            Hashtable result = new Hashtable();
            int i = 0;
            foreach (string value in table)
            {
                i++; result.Add(Convert.ToDouble(i), value);
            }
            return result;
        }
        Regex Init(string pattern)
        {
            Regex result;
            if (IgnoreCase)
                result = new Regex(pattern, RegexOptions.IgnoreCase);
            else
                result = new Regex(pattern);
            return result;
        }
    }
}
