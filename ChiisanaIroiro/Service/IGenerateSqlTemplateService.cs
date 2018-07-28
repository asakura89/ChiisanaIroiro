using System;

namespace ChiisanaIroiro.Service {
    public interface IGenerateSqlTemplateService
    {
        String GenerateActionTemplate();
        String GenerateRetrieveTemplate();
    }
}