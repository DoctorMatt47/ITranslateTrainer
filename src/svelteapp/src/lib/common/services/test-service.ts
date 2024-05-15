export type TestApiResponse = {
  id: number;
  text: string;
  answerTime: string,
  options: OptionApiResponse[];
}

export type OptionApiResponse = {
  id: number;
  text: string;
}

export type PutTestApiRequest = {
  fromLanguage: string;
  toLanguage: string;
  optionCount: number;
}

export type AnswerOnTestApiRequest = {
  optionId: number;
}

export type AnswerOnTestApiResponse = {
  correctOptionId: number;
}

const tests = "/tests";

export async function getTests(): Promise<TestApiResponse[]> {
  const response = await axiosDefaults.get(tests);
  return response.data;
}

export async function getTest(id: number): Promise<TestApiResponse> {
  const response = await axiosDefaults.get(`${tests}/${id}`);
  return response.data;
}

export async function putTest(request: PutTestApiRequest): Promise<TestApiResponse> {
  const response = await axiosDefaults.put(tests, request);
  return response.data;
}

export async function answerOnTest(id: number, request: AnswerOnTestApiRequest): Promise<AnswerOnTestApiResponse> {
  const response = await axiosDefaults.put(`${tests}/${id}/answer`, request);
  return response.data;
}
