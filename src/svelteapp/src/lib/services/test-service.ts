import { api } from "./api-service";

export interface TestResponse {
  id: number;
  string: string;
  answerTime: string,
  options: OptionResponse[];
}

export interface OptionResponse {
  id: number;
  string: string;
  isChosen: boolean;
  isCorrect: boolean;
}

export interface PutTestRequest {
  from: string;
  to: string;
  optionCount: number;
}

export interface AnswerOnTestRequest {
  optionId: number;
}

const tests = "/tests";

export async function getTests(): Promise<TestResponse[]> {
  const response = await api.get(tests);
  return response.data;
}

export async function getTest(id: number): Promise<TestResponse> {
  const response = await api.get(`${tests}/${id}`);
  return response.data;
}

export async function putTest(request: PutTestRequest): Promise<TestResponse> {
  const response = await api.put(tests, request);
  return response.data;
}

export async function answerOnTest(id: number, request: AnswerOnTestRequest) {
  await api.put(`${tests}/${id}/answer`, request);
}
