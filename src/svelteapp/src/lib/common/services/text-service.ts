import { axiosDefaults } from "./api-service";

export type TextApiRequest = {
  value: string;
  language: string;
}

export type TextApiResponse = {
  id: number;
  value: string;
  language: string;
}

export async function getTexts(): Promise<TextApiResponse[]> {
  const response = await axiosDefaults.get("/api/texts");
  return response.data;
}
