import { api } from "./api-service";

export interface TextRequest {
  value: string;
  language: string;
}

export interface TextResponse {
  id: number;
  value: string;
  language: string;
}

export async function getTexts(): Promise<TextResponse[]> {
  const response = await api.get("/api/texts");
  return response.data;
}
