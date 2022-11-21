import {baseUrl} from "../utils/env";
import axios from "axios";

export interface TestResponse {
  id: number;
  string: string;
  options: OptionResponse[];
}

export interface OptionResponse {
  id: number;
  string: string;
  isChosen: boolean;
  isCorrect: boolean;
}

export interface TestRequest {
  from: string;
  to: string;
  optionCount: number;
}

export interface AnswerOnTestRequest {
  optionId: number;
}

const apiTests = baseUrl + "/api/tests";

export const getTests = async (): Promise<TestResponse[]> => {
  const response = await axios.get(apiTests);
  return response.data;
};

export const getTest = async (id: number): Promise<TestResponse> => {
  const response = await axios.get(apiTests + `/${id}`);
  return response.data;
};

export const putTest = async (request: TestRequest) => {
  await axios.put(apiTests, request);
};

export const answerOnTest = async (
  id: number,
  request: AnswerOnTestRequest
) => {
  await axios.put(apiTests + `${id}/answer`, request);
};
