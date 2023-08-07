import { api } from "./api-service";

export interface TextResponse {
  id: number;
  string: string;
  language: string;
  canBeOption: boolean;
  canBeTested: boolean;
  correctCount: number;
  incorrectCount: number;
}

export async function getTexts(): Promise<TextResponse[]> {
  const response = await api.get("/api/texts");
  return response.data;
}
