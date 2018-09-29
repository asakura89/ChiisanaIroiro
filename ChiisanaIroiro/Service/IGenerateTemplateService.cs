using System;

namespace ChiisanaIroiro.Service {
    public interface IGenerateTemplateService {
        String GenerateActionTemplate();
        String GenerateRetrieveTemplate();
    }
}