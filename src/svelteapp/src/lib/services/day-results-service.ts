import { api } from "./api-service";

export interface DayResultResponse {
  day: string;
  correctCount: number;
  incorrectCount: number;
}

export async function getDayResults(): Promise<DayResultResponse[]> {
  const response = await api.get("/day-results");
  return response.data;
}
