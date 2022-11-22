import {TestResponse} from "../../../common/services/test-service";

interface TestResultProps {
  testResult: TestResponse;
}

const DayResult = ({testResult}: TestResultProps) => {
  return (
    <tr className="translation-row text-center">
    <td className="w-20">{testResult.answerTime}</td>
    <td className="w-20">{testResult.id}</td>
    <td className="w-20">{testResult.string}</td>
    <td className={color(isCorrect())}>{chosen()}</td>
    </tr>
);

  function chosen() : string {
    return testResult.options.find(o => o.isChosen)!.string;
  }

  function isCorrect() : boolean {
    return testResult.options.some(o => o.isCorrect && o.isChosen);
  }

  function color(isCorrect: boolean) : string {
    return ["w-30", isCorrect ? "text-success" : "text-danger"].join(" ")
  }
};

export default DayResult;
