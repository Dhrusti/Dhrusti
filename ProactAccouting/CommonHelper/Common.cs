using ProactAccouting.Models;
using ProactAccouting.RequestModel;

namespace ProactAccouting.CommonHelper
{
    public class Common
    {
        public enum AccountLevel
        {
            First = 1, Second = 2, Third = 3, Forth = 4, Fourth = 5
        }

        private readonly ProactAccountDbContext _dbcontext;
        public Common()
        {

        }
        public Common(ProactAccountDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public string GenerateCode(CodeGenerateReqModel codeGenerateReqModel)
        {
            string generatedCode = string.Empty;
            if (codeGenerateReqModel.LevelId >= 1 && codeGenerateReqModel.LevelId <= 5)
            {
                string lastCode = FindLastCode(codeGenerateReqModel);
                generatedCode = lastCode.All(char.IsDigit) == true ? GenerateNewCode(codeGenerateReqModel.LevelId, lastCode) : "Last code is not valid";
            }
            else
            {
                generatedCode = "Enter valid level";
            }

            return generatedCode;
        }
        public string FindLastCode(CodeGenerateReqModel codeGenerateReqModel)
        {
            string generatedCode = string.Empty, lastCode = string.Empty;

            switch (codeGenerateReqModel.LevelId)
            {
                case 1:
                    var caseOne = _dbcontext.LevelFirstMsts.OrderByDescending(x => x.LevelFirstId).FirstOrDefault() == null ? "000000000000000" : _dbcontext.LevelFirstMsts.OrderByDescending(x => x.LevelFirstId).FirstOrDefault().Code;
                    lastCode = caseOne.ToString();
                    break;
                case 2:
                    var caseTwo = _dbcontext.LevelSecondMsts.OrderByDescending(x => x.LevelSecondId).Where(x => x.LevelFirstId == codeGenerateReqModel.ParentId).FirstOrDefault() == null ? _dbcontext.LevelFirstMsts.FirstOrDefault(x => x.LevelFirstId == codeGenerateReqModel.ParentId).Code : _dbcontext.LevelSecondMsts.OrderByDescending(x => x.LevelSecondId).Where(x => x.LevelFirstId == codeGenerateReqModel.ParentId).FirstOrDefault().Code;
                    lastCode = caseTwo.ToString();
                    break;
                case 3:
                    var caseThree = _dbcontext.LevelThirdMsts.OrderByDescending(x => x.LevelThirdId).Where(x => x.LevelSecondId == codeGenerateReqModel.ParentId).FirstOrDefault() == null ? _dbcontext.LevelSecondMsts.FirstOrDefault(x => x.LevelSecondId == codeGenerateReqModel.ParentId).Code : _dbcontext.LevelThirdMsts.OrderByDescending(x => x.LevelSecondId).Where(x => x.LevelSecondId == codeGenerateReqModel.ParentId).FirstOrDefault().Code;
                    lastCode = caseThree.ToString();
                    break;
                case 4:
                    var caseFour = _dbcontext.LevelFourthMsts.OrderByDescending(x => x.LevelFourthId).Where(x => x.LevelThirdId == codeGenerateReqModel.ParentId).FirstOrDefault() == null ? _dbcontext.LevelThirdMsts.FirstOrDefault(x => x.LevelThirdId == codeGenerateReqModel.ParentId).Code : _dbcontext.LevelFourthMsts.OrderByDescending(x => x.LevelFourthId).Where(x => x.LevelThirdId == codeGenerateReqModel.ParentId).FirstOrDefault().Code;
                    lastCode = caseFour.ToString();
                    break;
                case 5:
                    var caseFive = _dbcontext.LevelFifthMsts.OrderByDescending(x => x.LevelFifthId).Where(x => x.LevelFourthId == codeGenerateReqModel.ParentId).FirstOrDefault() == null ? _dbcontext.LevelFourthMsts.FirstOrDefault(x => x.LevelFourthId == codeGenerateReqModel.ParentId).Code : _dbcontext.LevelFifthMsts.OrderByDescending(x => x.LevelFifthId).Where(x => x.LevelFourthId == codeGenerateReqModel.ParentId).FirstOrDefault().Code;
                    lastCode = caseFive.ToString();
                    break;
                default:
                    break;
            }
            return generatedCode = lastCode;

        }
        private string GenerateNewCode(int level, string lastCode)
        {
            string newCode = string.Empty;
            int chunkSize = 3;
            level--;
            string[] chunks = Enumerable.Range(0, lastCode.Length / chunkSize).Select(i => lastCode.Substring(i * chunkSize, chunkSize)).ToArray();
            for (int i = 0; i < chunks.Length; i++)
            {
                newCode += i == level ? (Convert.ToInt32(chunks[i]) + 1).ToString().PadLeft(3, '0') : chunks[i];
            }

            return newCode;

        }

    }
}
