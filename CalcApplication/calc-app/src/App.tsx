import React, {useState} from 'react';
import logo from './logo.svg';
import './App.css';
import amqp from 'amqplib/callback_api'

function App() {
    const [value, setValue] = useState('0')
    const [binArray, setBinArray] = useState('')
    const [system, setSystem] = useState('dec')
    const [wordSize, setWordSize] = useState('64')

    const send = async (message: string) => {
        amqp.connect('amqp://localhost', function(error0, connection) {
            if (error0) {
                throw error0;
            }
            connection.createChannel(function(error1, channel) {
                if (error1) {
                    throw error1;
                }
                var queue = 'gigacalc-input';
                var msg = message;

                channel.assertQueue(queue, {
                    durable: false
                });

                channel.sendToQueue(queue, Buffer.from(msg));
                console.log(" [x] Sent %s", msg);
            });
        });
    }

    send('1')


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
