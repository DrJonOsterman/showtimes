require 'open-uri'
require 'nokogiri'
doc = Nokogiri::HTML(open("movDump.htm"))
output = File.open("output.txt", "w")
doc.css("#movie_results .movie_results .theater").each do |theater|
	output.write("-------------------\n")
	output.write(theater.css(".desc .name a").text() + "(" + theater.css(".info")[0].text() + ")" + "\n")
	def printTimes(location, side, out)
		location.css(".showtimes .show_" + side + " .movie").each do |mov|
			out.write(mov.css(".name").text() + ": ")
			mov.css(".times span").each do |showTime|
				times = showTime.text().scan(/(\d{1,2}:\d\d[[ap]m]?)/)
				times.each do |time1|
					time1.each do |time2|
						out.write(time2 + " ")
					end
				end
			end
			out.write("\n")
		end
	end
	printTimes(theater, 'left', output)
	printTimes(theater, 'right', output)
end
output.close