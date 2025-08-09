export class ActionResponseViewModel {
    static readonly Info = "I";
    static readonly Warning = "W";
    static readonly Error = "E";
    static readonly Success = "S";

    responseType: string;
    message: string;

    constructor(responseType: string, message: string) {
        this.responseType = responseType;
        this.message = message;
    }

    toString(alwaysReturn: boolean = true): string {
        if (!alwaysReturn && this.responseType === ActionResponseViewModel.Error) {
            throw new Error(this.message);
        }

        return `${this.responseType}|${this.message}`;
    }
}

export function asActionResponseViewModelFromString(resultString: string, alwaysReturn: boolean = false): ActionResponseViewModel {
    if (resultString.length < 2) {
        throw new Error("resultString is bad formatted.");
    }

    const responseType = resultString.substring(0, 1);
    const message = resultString.substring(2);

    const allowedTypes = [
        ActionResponseViewModel.Info,
        ActionResponseViewModel.Warning,
        ActionResponseViewModel.Error,
        ActionResponseViewModel.Success,
    ];

    if (!allowedTypes.includes(responseType)) {
        throw new Error("resultString is bad formatted.");
    }

    const viewModel = new ActionResponseViewModel(responseType, message);

    if (!alwaysReturn && viewModel.responseType === ActionResponseViewModel.Error) {
        throw new Error(viewModel.message);
    }

    return viewModel;
}

export function asActionResponseViewModelFromException(ex: unknown): ActionResponseViewModel {
    let message = "Unknown error";
    if (ex instanceof Error) {
        message = ex.message;
    }
    else if (typeof ex === "string") {
        message = ex;
    }

    return new ActionResponseViewModel(ActionResponseViewModel.Error, message);
}