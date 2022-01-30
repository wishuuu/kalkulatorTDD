import React, {useState} from 'react';
import logo from './logo.svg';
import './App.css';

function App() {
    var amqp = require('amqplib/callback_api');
    
    const [value, setValue] = useState('0')
    const [binArray, setBinArray] = useState('')
    const [system, setSystem] = useState('dec')
    const [wordSize, setWordSize] = useState('64')

    amqp.connect('amqp://localhost', function(error0, connection) {
        if (error0) {
            throw error0;
        }
        connection.createChannel(function(error1, channel) {
            if (error1) {
                throw error1;
            }
            var queue = 'hello';
            var msg = 'Hello world';

            channel.assertQueue(queue, {
                durable: false
            });

            channel.sendToQueue(queue, Buffer.from(msg));
            console.log(" [x] Sent %s", msg);
        });
    });

    return (
        <div className="CalculatorArea">
            <div className="CalculatorValueArea">
                <div className="CalculatorValueBox">

                </div>
            </div>
        </div>
    );
}

export default App;
