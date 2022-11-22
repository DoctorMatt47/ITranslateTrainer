import {baseUrl} from "../utils/env";
import axios from "axios";

export interface DayResultResponse {
  day: string;
  correctCount: number;
  incorrectCount: number;
}

const apiDayResults = baseUrl + "/api/dayresults";

export const getDayResults = async () : Promise<DayResultResponse[]> => {
  const response = await axios.get(apiDayResults);
  return response.data;
};
