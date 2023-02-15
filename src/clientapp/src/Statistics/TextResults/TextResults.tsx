import Display from "../../common/components/Display/Display";
import {Col, Container, Row, Table} from "react-bootstrap";
import {useEffect, useState} from "react";
import {TextResponse} from "../../common/services/translation-service";
import {getTexts} from "../../common/services/text-service";
import TextResult from "./TextResult/TextResult";

const TextResults = () => {
  const [textResult, setTextResult] = useState<TextResponse[] | null>(null);
  useEffect(() => {
      getTexts()
        .then(t => setTextResult(t))
    },
    []);

  if (textResult === null)
    return (
      <div className="text-center fs-4">
        {"Loading..."}
      </div>
    );

  if (textResult.length === 0)
    return (
      <div className="text-center fs-4">
        You don't have any texts. Press "import" to add new
      </div>
    );

  return (
    <>
      <Display size={3} text="Text results" className="text-center mb-5"/>

      <div className="table-responsive">
        <Table className="translation-table align-middle">
          <tbody id="translations">
          {textResult.map((r, i) => <TextResult key={i} textResult={r}/>)}
          </tbody>
        </Table>
      </div>
    </>);
};

export default TextResults;
