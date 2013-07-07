require 'open-uri'
require 'nokogiri'

doc = Nokogiri::HTML(open("movDump.htm"))
output = File.open("output.txt", "w")

doc.css("#movie_results .movie_results .theater").each do |theater|

	output.write("-------------------\n")
	output.write(theater.css(".desc .name a").text() + "\n")

	theater.css(".showtimes .show_left .movie").each do |leftMov|
		output.write(leftMov.css(".name").text() + ": ")

		leftMov.css(".times span").each do |showTime|
			# add non-fandango movie logic here
			output.write(showTime.css("a").text() + " ")
		end
		output.write("\n")
	end

	# theater.css(".showtimes .show_right .movie").each do |rightMov|
	#  	output.write(rightMov.css(".name").text() + ": ")

	#  	rightMov.css(".times span").each do |showTime|
	# 		output.write(showTime.css("a").text() + " ")
	#  	end
	# 	output.write("\n")
	# end
end
output.close