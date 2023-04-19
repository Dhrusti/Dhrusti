using Microsoft.AspNetCore.Mvc;
using ProactAccouting.CommonHelper;
using ProactAccouting.Models;
using ProactAccouting.RequestModel;

namespace ProactAccouting.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FirstlevelController : ControllerBase
    {
        private readonly ProactAccountDbContext _dbContext;
        private readonly Common _common;
        public FirstlevelController(ProactAccountDbContext dbContext, Common common)
        {
            _dbContext = dbContext;
            _common = common;
        }


        [HttpPost]
        public ActionResult GenerateCode(CodeGenerateReqModel codeGenerateReqModel)
        {

            var generatedCode = _common.GenerateCode(codeGenerateReqModel);
            if (codeGenerateReqModel.LevelId == 1)
            {
                LevelFirstMst levelFirstMst = new LevelFirstMst();
                levelFirstMst.Code = generatedCode;
                levelFirstMst.CodeName = generatedCode;
                levelFirstMst.IsActive = true;
                levelFirstMst.IsDeleted = false;
                levelFirstMst.CreatedBy = 1;
                levelFirstMst.CreatedDate = DateTime.Now;
                levelFirstMst.UpdatedBy = 1;
                levelFirstMst.UpdatedDate = DateTime.Now;
                _dbContext.LevelFirstMsts.Add(levelFirstMst);
                _dbContext.SaveChanges();
            }
            else if (codeGenerateReqModel.LevelId == 2)
            {
                LevelSecondMst levelSecondMst = new LevelSecondMst();
                levelSecondMst.Code = generatedCode;
                levelSecondMst.CodeName = generatedCode;
                levelSecondMst.LevelFirstId = codeGenerateReqModel.ParentId;
                levelSecondMst.IsActive = true;
                levelSecondMst.IsDeleted = false;
                levelSecondMst.CreatedBy = 1;
                levelSecondMst.CreatedDate = DateTime.Now;
                levelSecondMst.UpdatedBy = 1;
                levelSecondMst.UpdatedDate = DateTime.Now;
                _dbContext.LevelSecondMsts.Add(levelSecondMst);
                _dbContext.SaveChanges();
            }
            else if (codeGenerateReqModel.LevelId == 3)
            {
                LevelThirdMst levelThirdMst = new LevelThirdMst();
                levelThirdMst.Code = generatedCode;
                levelThirdMst.CodeName = generatedCode;
                levelThirdMst.LevelSecondId = codeGenerateReqModel.ParentId;
                levelThirdMst.IsActive = true;
                levelThirdMst.IsDeleted = false;
                levelThirdMst.CreatedBy = 1;
                levelThirdMst.CreatedDate = DateTime.Now;
                levelThirdMst.UpdatedBy = 1;
                levelThirdMst.UpdatedDate = DateTime.Now;
                _dbContext.LevelThirdMsts.Add(levelThirdMst);
                _dbContext.SaveChanges();
            }
            else if (codeGenerateReqModel.LevelId == 4)
            {
                LevelFourthMst levelFourthMst = new LevelFourthMst();
                levelFourthMst.Code = generatedCode;
                levelFourthMst.CodeName = generatedCode;
                levelFourthMst.LevelThirdId = codeGenerateReqModel.ParentId;
                levelFourthMst.IsActive = true;
                levelFourthMst.IsDeleted = false;
                levelFourthMst.CreatedBy = 1;
                levelFourthMst.CreatedDate = DateTime.Now;
                levelFourthMst.UpdatedBy = 1;
                levelFourthMst.UpdatedDate = DateTime.Now;
                _dbContext.LevelFourthMsts.Add(levelFourthMst);
                _dbContext.SaveChanges();
            }
            else if (codeGenerateReqModel.LevelId == 5)
            {
                LevelFifthMst levelFifthMst = new LevelFifthMst();
                levelFifthMst.Code = generatedCode;
                levelFifthMst.CodeName = generatedCode;
                levelFifthMst.LevelFourthId = codeGenerateReqModel.ParentId;
                levelFifthMst.IsActive = true;
                levelFifthMst.IsDeleted = false;
                levelFifthMst.CreatedBy = 1;
                levelFifthMst.CreatedDate = DateTime.Now;
                levelFifthMst.UpdatedBy = 1;
                levelFifthMst.UpdatedDate = DateTime.Now;
                _dbContext.LevelFifthMsts.Add(levelFifthMst);
                _dbContext.SaveChanges();
            }
            else
            {
                generatedCode = "Enter valid level id";
            }
            return Ok(generatedCode);
        }
    }
}
