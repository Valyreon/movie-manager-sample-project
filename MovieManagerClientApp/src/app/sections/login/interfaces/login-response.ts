import { IBaseResponse } from "src/app/interfaces/base-response";

export interface ILoginResponse extends IBaseResponse {
  token: string;
}
