import Display from "../../common/components/Display/Display";
import {Col, Container, Row, Table} from "react-bootstrap";
import {useEffect, useState} from "react";
import {
  DayResultResponse,
  getDayResults
} from "../../common/services/day-results-service";
import DayResult from "./DayResult/DayResult";

const DayResults = () => {
  const [dayResults, setDayResults] = useState<DayResultResponse[] | null>(null);
  useEffect(() => {
      getDayResults()
        .then(r => setDayResults(r))
    },
    []);

  if (dayResults === null)
    return (
      <div className="text-center fs-4">
        {"Loading..."}
      </div>
    );

  if (dayResults.length === 0)
    return (
      <div className="text-center fs-4">
        You don't have any translations. Press "import" to add new
      </div>
    );

  return (
    <>
      <Display size={3} text="Day results" className="text-center mb-5"/>

      <div className="table-responsive">
        <Table className="translation-table align-middle">
          <tbody id="translations">
          {dayResults.map((r, i) => <DayResult key={i} dayResult={r}/>)}
          </tbody>
        </Table>
      </div>
    </>);

};

export default DayResults;
