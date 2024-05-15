import { axiosDefaults } from "./api-service";
import type { AxiosResponse } from "axios";

export type DayResultResponse
{
  string;
  number;
  number;
}

export async function getDayResults(): Promise<AxiosResponse<DayResultResponse[]>> {
  return await axiosDefaults.get("/day-results");
}
