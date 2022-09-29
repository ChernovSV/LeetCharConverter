using LeetCharConverter.Classes;
using LeetCharConverter.Enums;
using LeetCharConverter.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LeetCharConverter
{
    public partial class FormMain : Form
    {
        LatinInputWord _latinInputWord;
        List<string> LeetWords;

        public FormMain()
        {
            InitializeComponent();
            LoadData();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            long count = 0;
            int AllCount = 0;
            int cnt = 1;
            for (int i = 0; i < _latinInputWord.LatinLeetChars.Count(); i++)
            {
                if (_latinInputWord.LatinLeetChars[i].CountLeetWords > 0 && _latinInputWord.LatinLeetChars[i].OpenLetter)
                {
                    cnt *= _latinInputWord.LatinLeetChars[i].CountLeetWords;
                    AllCount += i == 0 ? _latinInputWord.LatinLeetChars[i].CountLeetWords : cnt;
                }
            }
            
            List<string> leetWords = new List<string>();
            List<string> newLeetWords;
            for (int i = 0; i < _latinInputWord.LatinLeetChars.Count(); i++)
            {
                newLeetWords = new List<string>();
                if (_latinInputWord.LatinLeetChars[i].CountLeetWords > 0 && _latinInputWord.LatinLeetChars[i].OpenLetter)
                {
                    for (int j = 0; j < _latinInputWord.LatinLeetChars[i].CountLeetWords; j++)
                    {
                        if (!leetWords.Any())
                        {
                            newLeetWords.Add(_latinInputWord.LatinLeetChars[i].LeetWords[j]);
                            count++;
                            var percents = (int)((count * 100) / AllCount);
                            backgroundWorker1.ReportProgress(percents, count);
                        }
                        else
                        {
                            for (int w = 0; w < leetWords.Count; w++)
                            {
                                newLeetWords.Add($"{leetWords[w]}{_latinInputWord.LatinLeetChars[i].LeetWords[j]}");
                                count++;
                                var percents = (int)((count * 100) / AllCount);
                                backgroundWorker1.ReportProgress(percents, count);
                            }
                        }
                    }
                }
                else
                {
                    if (!leetWords.Any())
                    {
                        newLeetWords.Add(_latinInputWord.LatinLeetChars[i].Letter.ToString());
                    }
                    else
                    {
                        for (int w = 0; w < leetWords.Count; w++)
                        {
                            newLeetWords.Add($"{leetWords[w]}{_latinInputWord.LatinLeetChars[i].Letter}");
                        }
                    }
                }
                leetWords = newLeetWords;
            }
            e.Result = leetWords;
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            btnRun.Enabled = true;
            btnRefresh.Enabled = true;

            LeetWords = (List<string>)e.Result;
            if (LeetWords.Any())
            {
                IOHelper.WriteLeetWords(LeetWords);
                MessageBox.Show("File creating complete!");
            }
            else
                MessageBox.Show("No words to create file");
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            btnRun.Enabled = false;
            btnRefresh.Enabled = false;
            backgroundWorker1.RunWorkerAsync();
        }

        private void LoadData()
        {
            progressBar1.Value = 0;
            _latinInputWord = null;
            LeetWords = new List<string>();

            var latinLeet = IOHelper.GetInputFiles();
            if (!latinLeet.Any())
            {
                MessageBox.Show("Not all files are correct in the 'input' directory");
                return;
            }

            _latinInputWord = IOHelper.GetLatinInputWord(latinLeet);
            if (!_latinInputWord.Correct)
            {
                MessageBox.Show("WordInLatin.txt isn't correct in the 'input' directory");
                _latinInputWord = null;
                return;
            }
        }
    }
}
